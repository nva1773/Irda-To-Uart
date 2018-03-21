//*****************************************************************************
// Receive NEC, SIRC and RC5 protocol IRDA and transmit 4 bytes to UART 9600bps 
//*****************************************************************************

/** I N C L U D E S **********************************************************/
#include <12f675.h>
#include "pic12f675.h"
#include "header.h"

/** C O N F I G U R A T I O N   B I T S **************************************/ 
// configuration bits: intrc_io, nomclr, wdt, nobrownout
#pragma fuses hs, nomclr, wdt, brownout, noprotect
// for used delay_mc(x) and delay_us(x))
#pragma use delay (clock = 16M)

/** I N T E R R U P T  V E C T O R S *****************************************/
// We use our interrupt function, allocating for it the program memory space
// using the preprocessor #org start, end
#pragma org 0x0004, 0x0014
void Interrupts(void);
void isr(void)
{
 ClrWdt();
 Interrupts();
 Retfie();
}

/** P R I V A T E  P R O T O T Y P E S ***************************************/
void InitCPU(void);
void InitVar(void);
void UART_Transmit(unsigned char data);
void IRDA_Start(void);
void IRDA_Stop(void);
byte NEC_Receive(void);
byte SIRC_Receive(void);
byte RC5_Receive(void);
void Delay_XX_10ms(byte time);

/** V A R I A B L E S ********************************************************/
byte Flag, StateIRDA, BitsCounter, ByteIRDA;
unsigned char DataUART[4]={0x4F,0x4B,0x4F,0x4B}; //"OKOK"
unsigned char DataIRDA[4]={0x01,0x02,0x03,0x04};
int16 TimerValue, SyncLength, DataLength;
enum stIRDA{
			stWait,
			stSyncStart,
			stSyncEnd,
			stNecAddr0,stNecAddr1,stNecData0,stNecData1,
			stSircData,stSircAddr,
			stRC5Addr,stRC5Data,
			stEnd};

// Bits
#bit fTransmitUART	= Flag. 0		// 1 - transmit data for UART
#bit fPulseIRDA		= Flag. 1		// 1 - receive pulse form IRDA
#bit fReceiveNEC	= Flag. 2		// 1 - receive NEC
#bit fReceiveSIRC	= Flag. 3		// 1 - receive SIRC
#bit fReceiveRC5	= Flag. 4		// 1 - receive RC5

/** I N T E R R U P T ********************************************************/
void Interrupts(void)
{
 // IRDA
 if(mInterruptIRDA)
 {
	 mFlagIRDA = false;
	 mChangeDirIRDA();
	 TimerValue = mReadTimer1();
	 mLoadTimer1();
	 mStartTimer1();
	 switch(StateIRDA)
	 {
		 case stWait:
		 {
			 StateIRDA = stSyncStart;
			 break;
		 }
		 // Start preambula
		 case stSyncStart:
		 {
			 SyncLength = TimerValue;
			 // NEC
			 if((SyncLength>NEC_SyncStartMin)&&(SyncLength<NEC_SyncStartMax))
			 {
				 StateIRDA = stSyncEnd;
				 fReceiveNEC = true;
				 break;
			 }
			 // SIRC
			 if((SyncLength>SIRC_SyncStartMin)&&(SyncLength<SIRC_SyncStartMax))
			 {
				 StateIRDA = stSyncEnd;
				 fReceiveSIRC = true;
				 break;
			 }
			 // RC5
			 if((SyncLength>RC5_SyncStartMin)&&(SyncLength<RC5_SyncStartMax))
			 {
				 StateIRDA = stSyncEnd;
				 fReceiveRC5 = true;
				 break;
			 }
			 //
			 IRDA_Start();
			 break;
		 }
		 // End preambula
		 case stSyncEnd:
		 {
			 SyncLength = TimerValue;
			 // NEC
			 if((SyncLength>NEC_SyncEndMin)&&(SyncLength<NEC_SyncEndMax)&&fReceiveNEC)
			 {
				 StateIRDA = stNecAddr0;
				 BitsCounter = 0;
				 ByteIRDA = 0;
				 break;
			 }
			 // SIRC
			 if((SyncLength>SIRC_SyncEndMin)&&(SyncLength<SIRC_SyncEndMax)&&fReceiveSIRC)
			 {
				 StateIRDA = stSircData;
				 BitsCounter = 0;
				 ByteIRDA = 0;
				 break;
			 }
			 // RC5
			 if((SyncLength>RC5_SyncEndMin)&&(SyncLength<RC5_SyncEndMax)&&fReceiveRC5)
			 {
				 StateIRDA = stRC5Addr;
				 BitsCounter = 0;
				 ByteIRDA = 0;
				 break;
			 }
		     //
		     IRDA_Start();
			 break;
		 }
		 // Receive NEC Addr0 = 0
		 case stNecAddr0:
		 // Receive NEC Addr1 = 1
		 case stNecAddr1:
		 // Receive NEC Data0 = 2
		 case stNecData0:
		 // Receive NEC Data1 = 3
		 case stNecData1:
		 {
			 if(NEC_Receive() == false) IRDA_Start();
			 break;
		 }
		 // Receive SIRC Data
		 case stSircData:
		 // Receive SIRC Addr
		 case stSircAddr:
		 {
			 if(SIRC_Receive() == false) IRDA_Start();
			 break;
		 }
		 // Receive RC5 Addr
		 case stRC5Addr:
		 // Receive RC5 Data
		 case stRC5Data:
		 {
			 if(RC5_Receive() == false) IRDA_Start();
			 else if(StateIRDA == stEnd)
			 {
				 IRDA_Stop();
			 }
			 break;
		 }
		 // Receive End
		 case stEnd:
		 {
			 IRDA_Stop();
			 break;
		 }
	 } 
  }
  
  // TMR1 - timeout
  if(mInterruptTimer1)
  {
	  mFlagTimer1 = false;
	  IRDA_Start();
  }
   
 }//end interrupt	 

/******************************************************************************
 * Function:        void main(void)
 *****************************************************************************/
void main(void)
{
 // Initialization CPU
 InitCPU();
 // Delay 1000 ms
 Delay_XX_10ms(100);
 // Initialization variable
 InitVar();
 // Interrupt enable
 mInterruptEnable();
 // Main cycle
 while(true)
 {
	 ClrWdt();
	 //
	 if(fTransmitUART)
	 {
		 //
		 mInterruptDisable();
		 //
 	     UART_Transmit(DataUART[0]);
		 UART_Transmit(DataUART[1]);
		 UART_Transmit(DataUART[2]);
		 UART_Transmit(DataUART[3]);
		 //
		 Delay_XX_10ms(50);
		 //
   	     IRDA_Start();
		 //
		 mInterruptEnable();
	 }
 }//end while
}

/******************************************************************************
 * Function:        static void InitCPU(void)
 *****************************************************************************/
void InitCPU(void)
{
	// PORTS
	mResetPorts();
	mPinsWPU(0xFE);
	mTurnOnWPU();
	// TIMER0
	mInitTimer0();
	mLoadTimer0();
	// TIMER1
	mInitTimer1();
	mLoadTimer1();
	// UART
	mInitUART();
	// IRDA
	mInitIRDA();
	// WDT
	ClrWdt();
}

/******************************************************************************
 * Function:        static void InitVar(void)
 *****************************************************************************/
void InitVar(void)
{
	Flag = 0;
	fTransmitUART = true; //Transmit "OKOK" to UART
	StateIRDA = stWait;
}

/******************************************************************************
 * Function:        static void UART_Transmit(unsigend char data)
 *
 * TX pin is usually high. A high to low bit is the starting bit and 
 * a low to high bit is the ending bit. No parity bit. No flow control.
 * BitCount is the number of bits to transmit. Data is transmitted LSB first.
 *****************************************************************************/
void UART_Transmit(unsigned char data)
{
	// Send Start Bit
	mLowTxD();
	delay_us(OneBitDelay);

	for (BitsCounter = 0; BitsCounter < UART_BitCount; BitsCounter++ )
	{
		//Set Data pin according to the data
		if( ((data>>BitsCounter)&0x1) == 0x1 )   //if Bit is high
		{
			mHighTxD();
		}
		else      //if Bit is low
		{
			mLowTxD();
		}

	    delay_us(OneBitDelay);
	}

	//Send Stop Bit
	mHighTxD();
	delay_us(OneBitDelay);
}

/******************************************************************************
 * Function:        static void Delay_XX_10ms(byte time)
 * Delay for 10 to 2500 ms
 *****************************************************************************/
void Delay_XX_10ms(byte time)
{
	byte i;
	for(i=0;i<time;i++)
	{
		ClrWdt();
		delay_ms(10);
	}
}

/******************************************************************************
 * Function:        static void IRDA_Start(void)
 *****************************************************************************/
void IRDA_Start(void)
{
	// Timer1
	mStopTimer1();
	mLoadTimer1();
	// State machine and counter bits
	StateIRDA = stWait;
	BitsCounter = 0;
	// Flags
	Flag = 0;
	// Extern interrupt from GP2 as falling eage
	mStartIRDA();
}

/******************************************************************************
 * Function:        static void IRDA_Stop(void)
 *****************************************************************************/
void IRDA_Stop(void)
{
	mStopIRDA();
	mStopTimer1();
	DataUART[0] = DataIRDA[0];
	DataUART[1] = DataIRDA[1];
	DataUART[2] = DataIRDA[2];
	DataUART[3] = DataIRDA[3];
    fTransmitUART = true;
}

/******************************************************************************
 * Function:        byte NEC_Receive(void)
 * 
 * Function receive bytes with IRDA: NEC = Addr0, Addr1. Data0, Data1.
 * Function return TRUE if receive OK, and return FALSE if receive FAULT.
 *****************************************************************************/
byte NEC_Receive(void)
{
 	byte retval;
 	retval = false;
 	//
 	if(!fPulseIRDA)
	{
		DataLength = TimerValue;
		fPulseIRDA = true;
		retval = true;
	}
	else
	{
		 fPulseIRDA = false;
		 DataLength += TimerValue;
		 BitsCounter++;
		 if((DataLength>NEC_Log0Min)&&(DataLength<NEC_Log0Max))
		 {
			 ByteIRDA = ByteIRDA>>1;
			 retval = true;
         }
         if((DataLength>NEC_Log1Min)&&(DataLength<NEC_Log1Max))
         {
	         ByteIRDA = ByteIRDA>>1;
	         ByteIRDA |= 0x80;
	         retval = true;
	     }
	}
    //
    if(BitsCounter >= NEC_BitCount)
    {
	    DataIRDA[StateIRDA - stNecAddr0] = ByteIRDA;
		BitsCounter = 0;
		ByteIRDA = 0;
		StateIRDA++;
		if(StateIRDA > stNecData1) StateIRDA = stEnd;
	} 
	//
	return retval;
}

/******************************************************************************
 * Function:        byte SIRC_Receive(void)
 * 
 * Function receive bytes with IRDA: SIRC = Command(7bit), Device(5bit).
 * Function return TRUE if receive OK, and return FALSE if receive FAULT.
 *****************************************************************************/
byte SIRC_Receive(void)
{
	byte retval;
 	retval = false;
 	//
	if(!fPulseIRDA)
	{
		DataLength = TimerValue;
		fPulseIRDA = true;
		retval = true;
	}
	else
	{
		 fPulseIRDA = false;
		 DataLength += TimerValue;
		 BitsCounter++;
		 if((DataLength>SIRC_Log0Min)&&(DataLength<SIRC_Log0Max))
		 {
			 ByteIRDA = ByteIRDA>>1;
			 retval = true;
         }
         if((DataLength>SIRC_Log1Min)&&(DataLength<SIRC_Log1Max))
         {
	         ByteIRDA = ByteIRDA>>1;
	         ByteIRDA |= 0x80;
	         retval = true;
	     }
	}
	//
    if(StateIRDA == stSircData)
    {
	    if(BitsCounter >= SIRC_DataBitCount)
	    {
		    DataIRDA[0] = ByteIRDA>>1;
		    DataIRDA[1] = 0;
		    BitsCounter = 0;
		    ByteIRDA = 0;
		    StateIRDA = stSircAddr;
		 }
	}
	else
	{
		if(BitsCounter >= SIRC_AddrBitCount)
	    {
		    DataIRDA[2] = ByteIRDA>>3;
		    DataIRDA[3] = 0;
		    BitsCounter = 0;
		    ByteIRDA = 0;
		    StateIRDA = stEnd;
		}
	}
	//
	return retval;
}		 

/******************************************************************************
 * Function:        byte RC5_Receive(void)
 * 
 * Function receive bytes with IRDA: RC5 = 1Start + 1Toggle + Addr(5bit), 
 *                                         + Data(6bit).
 * Function return TRUE if receive OK, and return FALSE if receive FAULT.
 *****************************************************************************/
byte RC5_Receive(void)
{
	byte retval;
 	retval = false;
 	// if Rising
    if(!fPulseIRDA)
	{
		fPulseIRDA = true;
		DataLength = TimerValue;
		//
		if((DataLength>RC5_PulseMin)&&(DataLength<RC5_PulseMax))
		{
			// if first start bit = 1
			if(BitsCounter == 0)
			{
				ByteIRDA = 0x01;
				BitsCounter = 1;
			}
			// else = 1 or 0
			else
			{
				BitsCounter++;
				ByteIRDA = ByteIRDA<<1;
				if((ByteIRDA & 0x02) == 0x02) ByteIRDA |= 0x01;
			}
			retval = true;
		}
		//
		if((DataLength>RC5_TooPulseMin)&&(DataLength<RC5_TooPulseMax))
		{
			// if first start + togel bit = 1 + 0
			if(BitsCounter == 0)
			{
				ByteIRDA = 0x02;
				BitsCounter = 2;
			}
			// else = 0
			else
			{
				BitsCounter++;
				ByteIRDA = ByteIRDA<<1;
			}
			retval = true;
		}
	}
	// if Faling
	else
	{
		fPulseIRDA = false;
		DataLength = TimerValue;
		//
		if((DataLength>RC5_PulseMin)&&(DataLength<RC5_PulseMax))
		{
			retval = true;
		}
		//
		if((DataLength>RC5_TooPulseMin)&&(DataLength<RC5_TooPulseMax))
		{
			// = 1
			BitsCounter++;
			ByteIRDA = ByteIRDA<<1;
			ByteIRDA |= 0x01;
			retval = true;
		}
	}
         
    //
    if(StateIRDA == stRC5Addr)
    {
	    if(BitsCounter >= RC5_AddrBitCount)
	    {
		    DataIRDA[0] = ByteIRDA & 0b00011111;
		    DataIRDA[1] = 0;
		    StateIRDA = stRC5Data;
		 }
	}
	else
	{
		if(BitsCounter >= RC5_DataBitCount)
	    {
		    DataIRDA[2] = ByteIRDA & 0b00111111;
		    DataIRDA[3] = 0;
		    BitsCounter = 0;
		    ByteIRDA = 0;
		    StateIRDA = stEnd;
		}
    }
	// 
	return retval;
}		 
