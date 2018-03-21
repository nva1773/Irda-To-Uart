#ifndef _HEADER_H
#define _HEADER_H

/** O S C ***********************************************************/
// OSC = 16 MHz
#define OSCILLATOR	16000000
#define INSTRUCTION_CYCLE (OSCILLATOR)/4

/** P O R T S ******************************************************/
#define mResetPorts() {CMCON = 0x07;		\
					   ANSEL = 0x00;		\
					   GPIO = 0xFF;			\
					   TRISIO = 0xFF;		\
					   WPU = 0x00;}
#define mTurnOnWPU() NOT_RBPU = 0
#define mTurnOffWPU() NOT_RBPU = 1
#define mPinsWPU(a) (WPU = a)

/** T I M E R 0 *****************************************************/
// Timer0 = 2 kHz (8 bits mode, Fosc/4, prescale 32)
#define TIMER0_PRESCALE 32
#define TIMER0_FREQUENCY 2000
#define TIMER0_CORRECTION 1
#define TMR0_VALUE 256-(INSTRUCTION_CYCLE/(TIMER0_FREQUENCY*TIMER0_PRESCALE))+TIMER0_CORRECTION
//#define TMR0_VALUE 132 //1000 mks
//#define TMR0_VALUE 189 //560 mks
#define mInitTimer0() OPTION_REG &= 0b11000000;OPTION_REG |= 0b00000100;T0IF = 0;T0IE = 0
#define mStartTimer0() T0IF = 0;T0IE = 1
#define mStopTimer0() T0IF = 0; T0IE = 0
#define mLoadTimer0() Nop();delay_us(3);TMR0 = TMR0_VALUE
#define mFlagTimer0 T0IF
#define mInterruptTimer0 T0IE&&T0IF
//
#define REPEAT		250			// 1ms*250 = 250mc

/** T I M E R  1 ****************************************************/
// Timer0 -> TimeOut = 12000mks (16 bits mode, Fosc/4, prescale 4)
#define TIMER1_PRESCALE 4
#define TIMER1_CYCLE 12000
#define TIMER1_CORRECTION 7
//#define TMR1_VALUE 65536-TIMER1_CYCLE+TIMER1_CORRECTION
#define TMR1_VALUE 53543
#define mInitTimer1() T1CON = 0b00100000;TMR1IF = 0;TMR1IE = 1
#define mStartTimer1() TMR1ON = 1
#define mStopTimer1() TMR1ON = 0; TMR1IF = 0
#define mLoadTimer1() TMR1 = TMR1_VALUE
#define mReadTimer1() TMR1 - TMR1_VALUE
#define mFlagTimer1 TMR1IF
#define mInterruptTimer1 TMR1IE&&TMR1IF

/** U A R T ********************************************************/
#define Baudrate 9600			//bps
#define Correct 7
#define OneBitDelay (1000000/Baudrate)-Correct
#define UART_BitCount 8
#define mUART_TXD GP0			// output TxD UART (Pin7)
// Output for RS232
#define mHighTxD() mUART_TXD = 0
#define mLowTxD() mUART_TXD = 1
//  Output for TTL
//#define mHighTxD() mUART_TXD = 1
//#define mLowTxD() mUART_TXD = 0
//
#define mInitUART() TRIS0 = 0; mHighTxD()

/** I R D A ********************************************************/
#define mIRDA_IN GP2			// input IrDa (Pin5)
#define mInitIRDA() TRIS2 = 1;
#define mRisingIRDA() INTEDG = 1
#define mFallingIRDA() INTEDG = 0
#define mChangeDirIRDA() INTEDG ^= 1
#define mStartIRDA() INTEDG = 0;INTIF = 0;INTIE = 1
#define mStopIRDA() INTIE = 0;INTIF = 0
#define mFlagIRDA INTIF
#define mInterruptIRDA INTIE&&INTIF

//-------------------------------------------------------------------
// NEC protocol coding duty (T = 560 mks)
// SyncStart = 9 ms = 9000 (+-20%)
#define NEC_SyncStartMin 7200
#define NEC_SyncStartMax 10800

// SyncEnd = 4,5 ms = 4500 (+-20%)
#define NEC_SyncEndMin 3600
#define NEC_SyncEndMax 5400

// Log1 = 4T = 2,24 ms = 2240 (+-25%)
#define NEC_Log1Min 1680
#define NEC_Log1Max 2800

// Log0 = 2T = 1,12 ms = 1120 (+-25%)
#define NEC_Log0Min 840
#define NEC_Log0Max 1400

// Number bits into Addr and Data
#define NEC_BitCount 8

//-------------------------------------------------------------------
// SIRC protocol coding duty (T = 600 mks)
// SyncStart = 4T = 2,4 ms = 2400 (+-20%)
#define SIRC_SyncStartMin 1920
#define SIRC_SyncStartMax 2880

// SyncEnd = T = 0,6 ms = 600 (+-20%)
#define SIRC_SyncEndMin 480
#define SIRC_SyncEndMax 720

// Log1 = 3T = 1,8 ms = 1800 (+-20%)
#define SIRC_Log1Min 1450
#define SIRC_Log1Max 2160

// Log0 = 2T =  1,2 ms = 1200 (+-20%)
#define SIRC_Log0Min 960
#define SIRC_Log0Max 1430

// Number bits Command and Device (12 bits)
#define SIRC_DataBitCount 7
#define SIRC_AddrBitCount 5

//-------------------------------------------------------------------
// RC5 protocol coding duty (T = 1778 mks)
// SyncStart = T/2 = 0.9 ms = 889 (+-10%)
#define RC5_SyncStartMin 810
#define RC5_SyncStartMax 980

// SyncEnd = T/2 = 0.9 ms = 889 (+-10%)
#define RC5_SyncEndMin 810
#define RC5_SyncEndMax 980

// Pulse = T/2 = 0.9 ms = 889 (+-10%)
#define RC5_PulseMin 810
#define RC5_PulseMax 980

// TooPulse = T = 1,778 ms = 1778 + 10%
#define RC5_TooPulseMin 1600
#define RC5_TooPulseMax 1960

// Number bits Start + Toggle + Addr(5bits) = 7, Data(6bits)
#define RC5_AddrBitCount 7
#define RC5_DataBitCount 6+RC5_AddrBitCount

/** I S R  *********************************************************/
#define mInterruptEnable()  PEIE = 1;	GIE = 1
#define mInterruptDisable()  PEIE = 0;	GIE = 0

#endif