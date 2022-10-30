/*
Name: ConnectionArduino.ino
Created: 11-Jun-18 2:33:38 PM
Author:	VasileDan
*/

#include <Wire.h>
#include <LCD.h>
#include <LiquidCrystal_I2C.h>
#include <dht.h>
#include <DueTimer.h>
#include <vector>
#include "BasicStepperDriver.h"
#include "MultiDriver.h"
#include "SyncDriver.h"
#include "FastLED.h"
#include "DRV8825.h"
#include <Thread.h>

//blindfolded Strings
// edges
String letterA = "L2 R2";
String letterB = "R' U R U' L2 R2 D R' D' R";
String letterB2m = "R' D R D' L2 R2 U R' U' R";

String letterC = "U2 L R' F2 L R'";
String letterC2m = "D2 L R' B2 L R'";

String letterD = "L U' L' U L2 R2 D' L D L'";
String letterD2m = "L D' L' D L2 R2 U' L U L'";

String letterE = "B L' B' L2 R2 F L F'";
String letterE2m = "F L' F' L2 R2 B L B'";

String letterF = "B L2 B' L2 R2 F L2 F'";
String letterF2m = "F L2 F' L2 R2 B L2 B'";

String letterG = "B L B' L2 R2 F L' F'";
String letterG2m = "F L F' L2 R2 B L' B'";

String letterH = "L' B L B' L2 R2 F L' F' L";
String letterH2m = "L' F L F' L2 R2 B L' B' L";

String letterI = "D L R' F R2 F' L' R U R2 U' D' L2 R2";
String letterI2m = "U L R' B R2 B' L' R D R2 D' U' L2 R2";

String letterJ = "U R U' L2 R2 D R' D'";
String letterJ2m = "D R D' L2 R2 U R' U'";

String letterL = "U' L' U L2 R2 D' L D";
String letterL2m = "D' L' D L2 R2 U' L U";

String letterM = "B' R B L2 R2 F' R' F";
String letterM2m = "F' R F L2 R2 B' R' B";

String letterN = "R B' R' B L2 R2 F' R F R'";
String letterN2m = "R F' R' F L2 R2 B' R B R'";

String letterO = "B' R' B L2 R2 F' R F";
String letterO2m = "F' R' F L2 R2 B' R B";

String letterP = "B' R2 B L2 R2 F' R2 F";
String letterP2m = "F' R2 F L2 R2 B' R2 B";

String letterQ = "B' R B U R2 U' L2 R2 D R2 D' F' R' F";
String letterQ2m = "F' R F D R2 D' L2 R2 U R2 U' B' R' B";

String letterR = "U' L U L2 R2 D' L' D";
String letterR2m = "D' L D L2 R2 U' L' U";

String letterS = "L2 R2 U D R2 D' L R' B R2 B' L' R U'";
String letterS2m = "L2 R2 D U R2 U' L R' F R2 F' L' R D'";

String letterT = "U R' U' L2 R2 D R D'";
String letterT2m = "D R' D' L2 R2 U R U'";

String letterV = "U R2 U' L2 R2 D R2 D'";
String letterV2m = "D R2 D' L2 R2 U R2 U'";

String letterW = "L' R B2 L' R D2";
String letterW2m = "L' R F2 L' R U2";

String letterX = "U' L2 U L2 R2 D' L2 D";
String letterX2m = "D' L2 D L2 R2 U' L2 U";
//parity
String parity2m = "U' L2 U L2 R2 D' L2 D";
//oldPochmann
String oldPochmannAlgorithm = "R U' R' U' R U R' F' R U R' U' R' F R";
//String oldPochmannAlgorithm = "--OP--";
//corners
String cornerB = "R D'";
String cornerB2b = "D R'";

String cornerC = "F";
String cornerC2b = "F'";

String cornerD = "F R'";
String cornerD2b = "R F'";

String cornerF = "F2";
//string cornerF2b = "F2";

String cornerG = "F2 R'";
String cornerG2b = "R F2";

String cornerH = "D2";
//string cornerH2b = "D2";

String cornerI = "F' D";
String cornerI2b = "D' F";

String cornerJ = "F2 D";
String cornerJ2b = "D' F2";

String cornerK = "F D";
String cornerK2b = "D' F'";

String cornerL = "D";
String cornerL2b = "D'";

String cornerM = "R'";
String cornerM2b = "R";

String cornerN = "R2";
//string cornerN2b = "R2";

String cornerO = "R";
String cornerO2b = "R'";

String cornerP = "";
String cornerP2b = "";

String cornerQ = "R' F";
String cornerQ2b = "F' R";

String cornerS = "D' R";
String cornerS2b = "R' D";

String cornerT = "D'";
String cornerT2b = "D";

String cornerU = "F'";
String cornerU2b = "F";

String cornerV = "D' F'";
String cornerV2b = "F D";

String cornerW = "D2 F'";
String cornerW2b = "F D2";

String cornerX = "D F'";
String cornerX2b = "F D'";

// patterns 
String symmetryH[8] = { "U2","L2","F2","D2","U2","F2","R2","U2" };
String symmetryDot[8] = { "B'","D'","U","L'","R","B'","F","U" };
String symmetryChessTable[6] = { "R2","L2","U2","D2","F2","B2" };
String symmetrySnake[12] = { "R2","F2","U'","D'","B2","L2","F2","L2","U","D","R2","F2" };
String symmetryTwistedPeaks[17] = { "F","B'","U","F","U","F","U","L","B","L2","B'","U","F'","L","U","L'","B" };
String symmetryTwistedRings[16] = { "F","D","F'","D2","L'","B'","U","L","D","R","U","L'","F'","U","L","U2" };
String symmetryTwistedChickenFeet[16] = { "F","L'","D","F'","U'","B","U","F","U'","F","R'","F2","L","U'","R'","D2" };
String symmetryCubeinCube[15] = { "F","L","F","U'","R","U","F2","L2","U'","L'","B","D'","B'","L2","U" };
String symmetryAnaconda[14] = { "L","U","B'","U'","R","L'","B","R'","F","B'","D","R","D'","F'" };
String symmetryUnionjack[11] = { "U","F","B'","L2","U2","L2","F'","B","U2","L2","U" };

String WarmUpPattern[14] = { "U","U'","F2","F2","B","B'","R","L","R'","L'","D2","D","D'","D2"};

// leds
#define NUM_LEDS 96
#define DATA_PIN 24

CRGB leds[NUM_LEDS];
bool turnOnOffLeds = true;
bool onOffDownRing = false;
int ledsArray[24] = { 1,9,22,30,38,46,54,62,70,78,81,89,6,14,17,25,33,41,65,73,86,94 };
int ledsArrayDownRing[12] = { 0,2,3,4,5,7,8,10,11,12,13,15};

// liquid crystal display
#define I2C_ADDR 0x27
#define BACKLIGHT_PIN 3
#define En_pin 2
#define Rw_pin 1
#define Rs_pin 0
#define D4_pin 4
#define D5_pin 5
#define D6_pin 6
#define D7_pin 7

LiquidCrystal_I2C lcd(I2C_ADDR, En_pin, Rw_pin, Rs_pin, D4_pin, D5_pin, D6_pin, D7_pin);

// serial connection
String inputString = "";
boolean stringComplete = false;
String commandString = "";
String chosenButtonComand = "";

//timer
#define latchPin 2
#define dataPin 3
#define clockPin 4
#define D1 3584
#define D2 3328
#define D3 2816
#define D4 1792
bool startStopTimer = false;
long n = 0;

int count = 0;
int countForShow = 0;
char switchDot = 'a';
int minutesCount = 0;
//

//-----motors setup----
#define MOTOR_STEPS 200
#define RPM 250
#define MICROSTEPS 1
//up and down motors microstepping
#define MICROSTEPSUD 16
#define MOTOR_ACCEL 500
#define MOTOR_DECEL 500
#define RPMUD 250

// motor 1 (spate)
#define DIR1 53 
#define STEP1 52
#define ENABLE1 41

//motor 2 (jos a.k.a baza)
#define DIR2 51
#define STEP2 50
#define ENABLE2 40

//motor 3 (dreapta)
#define DIR3 49
#define STEP3 48
#define ENABLE3 39

//motor 4 (sus)
#define DIR4 47
#define STEP4 46
#define ENABLE4 38

//motor 5 (stanga)
#define DIR5 45
#define STEP5 44
#define ENABLE5 37

//motor 6 (fata)
#define DIR6 43
#define STEP6 42
#define ENABLE6 36

// motor jos
#define DIRU 35
#define STEPU 34
#define ENABLEU 33
#define MODEU 32

//motor sus
#define DIRS 31
#define STEPS 30
#define ENABLES 29
#define MODES 28

DRV8825 stepper1Back(MOTOR_STEPS, DIR1, STEP1, ENABLE1);
DRV8825 stepper2Down(MOTOR_STEPS, DIR2, STEP2, ENABLE2);
DRV8825 stepper3Right(MOTOR_STEPS, DIR3, STEP3, ENABLE3);
DRV8825 stepper4Up(MOTOR_STEPS, DIR4, STEP4, ENABLE4);
DRV8825 stepper5Left(MOTOR_STEPS, DIR5, STEP5, ENABLE5);
DRV8825 stepper6Front(MOTOR_STEPS, DIR6, STEP6, ENABLE6);
DRV8825 stepperDown(MOTOR_STEPS, DIRU, STEPU, ENABLEU, LOW, LOW, MODEU);
DRV8825 stepperUp(MOTOR_STEPS, DIRS, STEPS, ENABLES, LOW, LOW, MODES);

SyncDriver controllerBackFront(stepper1Back, stepper6Front);
SyncDriver controllerDownUp(stepper2Down, stepper4Up);
SyncDriver controllerRightLeft(stepper3Right, stepper5Left);
MultiDriver controllerGripRelease(stepperDown, stepperUp);

// solutions strings
String kociembaSolutionString = "";
String blindfoldedCornersSolutionString = "";
String blindfoldedEdgesSolutionString = "";
// strings with final moves
String cornersFinalString = "";
String edgesFinalString = "";
//

int kociembaMoves = 0;
// array of full string received from pc
String kociembaArray[20] = {};

String blindfoldedCornersArray[30] = {};
String blindfoldedEdgesArray[30] = {};
// arrays of full final solution from blindfolded
String blindfoldedCornersArrayForSteppers[250] = {};
String blindfoldedEdgesArrayForSteppers[200] = {};
String parityArray[8] = { "U'","L2","U","L2","R2","D'","L2","D" };
String blindfoldedCombinedArrayForSteppers[450] = {};
int sumAll = 0;

//char arrays from pc
char bldCornersArray[60] = {};
char bldEdgesArray[60] = {};

int KmbBldMoves[5] = {};
int KmbBldFullStringLength[5] = {};

bool parityCheck = false;

//temperature and humidity
dht DHT;

#define DHT11_PIN 22

String temperature = "";
String humidity = "";
bool senzorPer = false;

//switchmode
int chosenMode = 0;

//random number
long randomNumber;


void temperatureAndHumidityUpdate() {
	senzorPer = true;
	if (temperature != "" && humidity != "")
	{
		lcd.setCursor(0, 1);
		lcd.print("  T:");
		lcd.print(temperature.substring(0, 2));
		lcd.print((char)223);
		lcd.print("C H:");
		lcd.print(humidity.substring(0,2));
		lcd.print("%");
	}
	//Serial.println("#T" + temperature + "H" + humidity + "K" + (String)KmbBldMoves[0] + "C" + (String)KmbBldMoves[1] + "E" + (String)KmbBldMoves[2] + "k" + (String)KmbBldFullStringLength[0] + "c" + (String)KmbBldFullStringLength[1] + "e" + (String)KmbBldFullStringLength[2] + "D" + "falseX");
}

void setup() {
	Serial.begin(9600);
	FastLED.addLeds<WS2812, DATA_PIN, RGB>(leds, NUM_LEDS);

	pinMode(latchPin, OUTPUT);
	pinMode(dataPin, OUTPUT);
	pinMode(clockPin, OUTPUT);

	stepper1Back.begin(RPM, MICROSTEPS);
	stepper2Down.begin(RPM, MICROSTEPS);
	stepper3Right.begin(RPM, MICROSTEPS);
	stepper4Up.begin(RPM, MICROSTEPS);
	stepper5Left.begin(RPM, MICROSTEPS);
	stepper6Front.begin(RPM, MICROSTEPS);

	stepperDown.begin(RPMUD, MICROSTEPSUD);
	stepperUp.begin(RPMUD, MICROSTEPSUD);

	stepper1Back.disable();
	stepper2Down.disable();
	stepper3Right.disable();
	stepper4Up.disable();
	stepper5Left.disable();
	stepper6Front.disable();

	stepperUp.disable();
	stepperDown.disable();

	stepperDown.setSpeedProfile(stepperDown.LINEAR_SPEED, MOTOR_ACCEL, MOTOR_DECEL);
	stepperUp.setSpeedProfile(stepperUp.LINEAR_SPEED, MOTOR_ACCEL, MOTOR_DECEL);

	Timer1.attachInterrupt(add).setFrequency(10000).start();

	Timer3.attachInterrupt(temperatureAndHumidityUpdate);
	Timer3.start(5000000);

	randomSeed(analogRead(0));

	initializeDisplay();
}

void loop() {

	showOnDisplayAndSwichDot();
	
	if (turnOnOffLeds == true) {
		for (int j = 0; j<22; j++) {
			leds[ledsArray[j]] = CRGB::White;
		}
	}
	else if (turnOnOffLeds == false) {
		for (int j = 0; j<22; j++) {
			leds[ledsArray[j]] = CRGB::Black;
		}
	}
	if (onOffDownRing == true)
	{
		for (int k = 0; k<12; k++) {
			leds[ledsArrayDownRing[k]] = CRGB::Green;
		}
	}
	else if (onOffDownRing == false) {
		for (int k = 0; k < 12; k++) {
			leds[ledsArrayDownRing[k]] = CRGB::Black;
		}
	}

	if (senzorPer == true)
	{
		int chk = DHT.read11(DHT11_PIN);
		temperature = (String)DHT.temperature;
		humidity = (String)DHT.humidity;
		senzorPer = false;
	}

	if (stringComplete)
	{
		stringComplete = false;
		getCommand();
		buttonCommand();

		if (commandString.equals("STAR"))
		{
			lcd.clear();
		}
		else if (commandString.equals("STOP"))
		{
			lcd.clear();
			lcd.print("Ready to connect!");
		}
		else if (commandString.equals("SOLC"))
		{
			if (chosenButtonComand.equals("KMB")) {
				solveTheCubeUsingKociemba();
			}
			else if (chosenButtonComand.equals("BLD"))
			{
				solveTheCubeUsingBlindfolded();
			}
			else if (chosenButtonComand.equals("SCM"))
			{
				//scrambleTheCube();
				warmUpPatternSteppers();
			}
			else if (chosenButtonComand.equals("RDM"))
			{
				generateRandomPattern();
			}
		}
		else if (commandString.equals("GRPC"))
		{
			if (chosenButtonComand.equals("GRP"))
			{
				gripTheCube();
			}
			else if (chosenButtonComand.equals("REL"))
			{
				releaseTheCube();
			}
		}
		else if (commandString.equals("MODE"))
		{
			if (chosenButtonComand.equals("AUT"))
			{
				chosenMode = 0;
			}
			else if (chosenButtonComand.equals("MAN"))
			{
				chosenMode = 1;
				manSemiAutomaticGrip();
			}
			else if (chosenButtonComand.equals("SAU"))
			{
				chosenMode = 2;
				manSemiAutomaticGrip();
			}
		}
		else if (commandString.equals("SPDM")) {
			changeSteppersRPM(chosenButtonComand.toInt());
		}

		if (commandString.equals("KMBA"))
		{
			KmbBldMoves[0] = 0;
			memset(kociembaArray, 0, sizeof(kociembaArray));
			kociembaSolutionString = inputString;
			KmbBldStringProccesing(kociembaSolutionString, kociembaArray, "KMB");
		}
		else if (commandString.equals("BLDC"))
		{
			parityCheck = false;
			KmbBldMoves[1] = 0;
			memset(blindfoldedCornersArray, 0, sizeof(blindfoldedCornersArray));
			blindfoldedCornersSolutionString = inputString;
			//Serial.print("\r\n Input string: "+blindfoldedCornersSolutionString);
			KmbBldStringProccesing(blindfoldedCornersSolutionString, blindfoldedCornersArray, "BLDC");
		}
		else if (commandString.equals("BLDE"))
		{
			parityCheck = false;
			KmbBldMoves[2] = 0;
			memset(blindfoldedEdgesArray, 0, sizeof(blindfoldedEdgesArray));
			blindfoldedEdgesSolutionString = inputString;
			KmbBldStringProccesing(blindfoldedEdgesSolutionString, blindfoldedEdgesArray, "BLDE");
		}
		else if (commandString.equals("KBDD"))
		{
			KmbBldMoves[4] = 0;
			KmbBldMoves[3] = 0;

			memset(blindfoldedCornersArrayForSteppers, 0, sizeof(blindfoldedCornersArrayForSteppers));
			memset(blindfoldedEdgesArrayForSteppers, 0, sizeof(blindfoldedEdgesArrayForSteppers));
			memset(blindfoldedCombinedArrayForSteppers, 0, sizeof(blindfoldedCombinedArrayForSteppers));

			KmbBldStringProccesing("#MDTT" + cornersFinalString + "#", blindfoldedCornersArrayForSteppers, "BLDCN");
			KmbBldStringProccesing("#MDTT" + edgesFinalString + "#", blindfoldedEdgesArrayForSteppers, "BLDEN");
			
			/*Serial.println();
			for (int i = 0; i < KmbBldMoves[3]; i++)
			{
				Serial.print(blindfoldedCornersArrayForSteppers[i]+",");
			}*/
			
			bldCombinedSteppers();
			onOffDownRing = true;
			lcd.setCursor(0, 0);
			lcd.print("P"+(String)parityCheck+"C" + (String)KmbBldMoves[3] + "E"+ (String)KmbBldMoves[4]+ "S"+ (String)sumAll);
		}
		inputString = "";
	}

	FastLED.show();
	
}
void theCubeIsSolved() {
	Serial.println("#K" + (String)KmbBldMoves[0] + "C" + (String)KmbBldMoves[1] + "E" + (String)KmbBldMoves[2] + "k" + (String)KmbBldFullStringLength[0] + "c" + (String)KmbBldFullStringLength[1] + "e" + (String)KmbBldFullStringLength[2] + "D" + "trueX");
}
void initializeDisplay() {
	lcd.begin(16, 2);
	lcd.setBacklightPin(BACKLIGHT_PIN, POSITIVE);
	lcd.setBacklight(HIGH);

	lcd.setCursor(0, 0);
	lcd.print(" Arcas v1.0 MDT");

	lcd.setCursor(0, 1);
	lcd.print("      ^_^");
}

void buttonCommand() {
	if (inputString.length() > 0)
	{
		chosenButtonComand = inputString.substring(5, 8);
	}
	lcd.setCursor(0, 0);
	lcd.print("                ");
	lcd.setCursor(0, 0);
	lcd.print(chosenButtonComand);
}

void getCommand()
{
	if (inputString.length()>0)
	{
		commandString = inputString.substring(1, 5);
	}
}

void solveTheCubeUsingKociemba() {
	resetTimer();
	startStopTimer = true;
	holdOnUpAndDown();
	//steppersSolveTheCubeUN(kociembaArray, 20);
	steppersSolveTheCubeOptimized(kociembaArray,20);
	showOnDisplayAndSwichDot();
	startStopTimer = false;
	releaseUpAndDown();
	showOnDisplayAndSwichDot();
	theCubeIsSolved();
	releaseTheCube();
}

void solveTheCubeUsingBlindfolded() {
	resetTimer();
	startStopTimer = true;
	holdOnUpAndDown();
	//steppersSolveTheCubeUN(blindfoldedCombinedArrayForSteppers, sumAll);
	steppersSolveTheCubeOptimized(blindfoldedCombinedArrayForSteppers,450);
	startStopTimer = false;
	releaseUpAndDown();
	showOnDisplayAndSwichDot();
	theCubeIsSolved();
	releaseTheCube();
}

void scrambleTheCube() {
	resetTimer();
	startStopTimer = true;
	holdOnUpAndDown();
	randomCubeState();
	startStopTimer = false;
	releaseUpAndDown();
	showOnDisplayAndSwichDot();
	theCubeIsSolved();
	releaseTheCube();
}

void generateRandomPattern() {
	resetTimer();
	startStopTimer = true;
	holdOnUpAndDown();
	generateRandomAlgorithm();
	startStopTimer = false;
	releaseUpAndDown();
	showOnDisplayAndSwichDot();
	theCubeIsSolved();
	releaseTheCube();
}

void warmUpPatternSteppers() {
	holdOnUpAndDown();
	steppersSolveTheCubeOptimized(WarmUpPattern, 14);
	releaseUpAndDown();
	showOnDisplayAndSwichDot();
	theCubeIsSolved();
	releaseTheCube();
	warmUpRepeatAnaconda();
	warmUpRepeatAnaconda();
}

void warmUpRepeatAnaconda() {
	gripTheCube();
	holdOnUpAndDown();
	steppersSolveTheCubeOptimized(symmetryChessTable, 6);
	releaseUpAndDown();
	showOnDisplayAndSwichDot();
	theCubeIsSolved();
	releaseTheCube();
}

void manSemiAutomaticGrip() {
	stepperUp.rotate(-180);
	stepperUp.disable();
	stepperDown.disable();
}

void holdOnUpAndDown() {
	stepperUp.enable();
	stepperDown.enable();
}
void releaseUpAndDown() {
	stepperUp.disable();
	stepperDown.disable();
}


void generateRandomAlgorithm() {
	randomNumber = random(10);

	switch (randomNumber)
	{
	case 0:
		steppersSolveTheCubeOptimized(symmetryCubeinCube, 15);
		break;
	case 1:
		steppersSolveTheCubeOptimized(symmetryH, 8);
		break;
	case 2:
		steppersSolveTheCubeOptimized(symmetryDot, 8);
		break;
	case 3:
		steppersSolveTheCubeOptimized(symmetryChessTable, 6);
		break;
	case 4:
		steppersSolveTheCubeOptimized(symmetrySnake, 12);
		break;
	case 5:
		steppersSolveTheCubeOptimized(symmetryTwistedPeaks, 17);
		break;
	case 6:
		steppersSolveTheCubeOptimized(symmetryTwistedRings, 16);
		break;
	case 7:
		steppersSolveTheCubeOptimized(symmetryTwistedChickenFeet, 16);
		break;
	case 8:
		steppersSolveTheCubeOptimized(symmetryAnaconda, 14);
		break;
	case 9:
		steppersSolveTheCubeOptimized(symmetryUnionjack, 11);
		break;
	default:
		break;
	}
}

void randomCubeState() {
	for (int i = 0; i < 21; i++)
	{
		randomNumber = random(18);
		switch (randomNumber)
		{
		case 0:
			stepper6Front.enable();
			stepper6Front.rotate(90);
			stepper6Front.disable();
			break;
		case 1:
			stepper1Back.enable();
			stepper1Back.rotate(90);
			stepper1Back.disable();
			break;
		case 2:
			stepper3Right.enable();
			stepper3Right.rotate(90);
			stepper3Right.disable();
			break;
		case 3:
			stepper5Left.enable();
			stepper5Left.rotate(90);
			stepper5Left.disable();
			break;
		case 4:
			stepper4Up.enable();
			stepper4Up.rotate(90);
			stepper4Up.disable();
			break;
		case 5:
			stepper2Down.enable();
			stepper2Down.rotate(90);
			stepper2Down.disable();
			break;
		case 6:
			stepper6Front.enable();
			stepper6Front.rotate(-90);
			stepper6Front.disable();
			break;
		case 7:
			stepper1Back.enable();
			stepper1Back.rotate(-90);
			stepper1Back.disable();
			break;
		case 8:
			stepper3Right.enable();
			stepper3Right.rotate(-90);
			stepper3Right.disable();
			break;
		case 9:
			stepper5Left.enable();
			stepper5Left.rotate(-90);
			stepper5Left.disable();
			break;
		case 10:
			stepper4Up.enable();
			stepper4Up.rotate(-90);
			stepper4Up.disable();
			break;
		case 11:
			stepper2Down.enable();
			stepper2Down.rotate(-90);
			stepper2Down.disable();
			break;
		case 12:
			stepper6Front.enable();
			stepper6Front.rotate(180);
			stepper6Front.disable();
			break;
		case 13:
			stepper1Back.enable();
			stepper1Back.rotate(180);
			stepper1Back.disable();
			break;
		case 14:
			stepper3Right.enable();
			stepper3Right.rotate(180);
			stepper3Right.disable();
			break;
		case 15:
			stepper5Left.enable();
			stepper5Left.rotate(180);
			stepper5Left.disable();
			break;
		case 16:
			stepper4Up.enable();
			stepper4Up.rotate(180);
			stepper4Up.disable();
			break;
		case 17:
			stepper2Down.enable();
			stepper2Down.rotate(180);
			stepper2Down.disable();
			break;
		default:
			break;
		}
	}
}

void serialEventRun(void) {
	if (Serial.available()) serialEvent();
}

void serialEvent() {
	while (Serial.available())
	{
		char inChar = (char)Serial.read();
		inputString += inChar;
		if (inChar == '\n')
		{
			stringComplete = true;
		}
	}
}

// Strings proccesing

void KmbBldNumberOfMoves() {
	kociembaMoves = inputString.substring(5, inputString.length() - 2).toInt();
}

void KmbBldStringProccesing(String solutionString, String arrayToSave[], String methodToSolve) {
	solutionString = solutionString.substring(5, solutionString.length() - 2);
	//Serial.print("\r\n Solution String Cut:"+solutionString);
	solutionToArrayV(solutionString, arrayToSave, methodToSolve);
}

void solutionToArrayV(String algorithm, String arrayToProcces[], String methodUsed) {
	int spliterCount = 0, totalBLD = 0;
	int spliterArray[400] = {};

	for (int i = 0; i < algorithm.length(); i++) {
		if (algorithm.substring(i, i + 1) == " ")
		{
			spliterArray[spliterCount] = algorithm.substring(0, i).length();
			spliterCount++;
		}
	}

	for (int m = 0; m < (spliterCount + 1); m++)
	{
		if (m == 0) {
			arrayToProcces[m] = algorithm.substring(0, spliterArray[m]);
		}
		else if (m == spliterCount) {
			arrayToProcces[m] = algorithm.substring(spliterArray[m - 1] + 1, algorithm.length());
		}
		else
		{
			arrayToProcces[m] = algorithm.substring(spliterArray[m - 1] + 1, spliterArray[m]);
		}
	}


	if (methodUsed.equals("KMB")) {
		KmbBldMoves[0] = spliterCount + 1;
		KmbBldFullStringLength[0] = algorithm.length();
	}
	else if (methodUsed.equals("BLDC"))
	{
		KmbBldMoves[1] = algorithm.length();
		KmbBldFullStringLength[1] = KmbBldMoves[1];
		stringArrayToCharArray(algorithm, "corners");
	}
	else if (methodUsed.equals("BLDE"))
	{
		KmbBldMoves[2] = algorithm.length();
		KmbBldFullStringLength[2] = KmbBldMoves[2];
		stringArrayToCharArray(algorithm, "edges");
	}
	else if (methodUsed.equals("BLDCN"))
	{
		KmbBldMoves[3] = spliterCount + 1;
		KmbBldFullStringLength[3] = algorithm.length();
	}
	else if (methodUsed.equals("BLDEN"))
	{
		KmbBldMoves[4] = spliterCount + 1;
		KmbBldFullStringLength[4] = algorithm.length();
	}

	Serial.println("#K" + (String)KmbBldMoves[0] + "C" + (String)KmbBldMoves[1] + "E" + (String)KmbBldMoves[2] + "k" + (String)KmbBldFullStringLength[0] + "c" + (String)KmbBldFullStringLength[1] + "e" + (String)KmbBldFullStringLength[2] + "D" + "falseX");
}


void checkArrayLength(String arrayLength[], int delimiterA) {
	for (int m = 0; m < (delimiterA + 1); m++) {
		Serial.print(arrayLength[m] + " ");
	}

	for (int m = 0; m < (delimiterA + 1); m++) {
		Serial.print("\r\n [" + (String)m + "] : " + arrayLength[m]);
	}

	Serial.print("\r\nDelimiter length: " + (String)(delimiterA)+"\r\n");
}

void steppersSolveTheCubeUN(String solutionArray[], int length) {
	steppersEnable();
	for (int i = 0; i < length; i++)
	{
		if (solutionArray[i].equals("F")) {
			//stepper6Front.enable();
			stepper6Front.rotate(90);
			//stepper6Front.disable();
		}
		else if (solutionArray[i].equals("B")) {
			//stepper1Back.enable();
			stepper1Back.rotate(90);
			//stepper1Back.disable();
		}
		else if (solutionArray[i].equals("R")) {
			//stepper3Right.enable();
			stepper3Right.rotate(90);
			//stepper3Right.disable();
		}
		else if (solutionArray[i].equals("L")) {
			//stepper5Left.enable();
			stepper5Left.rotate(90);
			//stepper5Left.disable();
		}
		else if (solutionArray[i].equals("U")) {
			//stepper4Up.enable();
			stepper4Up.rotate(90);
			//stepper4Up.disable();
		}
		else if (solutionArray[i].equals("D")) {
			//stepper2Down.enable();
			stepper2Down.rotate(90);
			//stepper2Down.disable();
		}

		else if (solutionArray[i].equals("F'")) {
		//	stepper6Front.enable();
			stepper6Front.rotate(-90);
			//stepper6Front.disable();
		}
		else if (solutionArray[i].equals("B'")) {
			//stepper1Back.enable();
			stepper1Back.rotate(-90);
			//stepper1Back.disable();
		}
		else if (solutionArray[i].equals("R'")) {
			//stepper3Right.enable();
			stepper3Right.rotate(-90);
			//stepper3Right.disable();
		}
		else if (solutionArray[i].equals("L'")) {
			//stepper5Left.enable();
			stepper5Left.rotate(-90);
		//	stepper5Left.disable();
		}
		else if (solutionArray[i].equals("U'")) {
			//stepper4Up.enable();
			stepper4Up.rotate(-90);
			//stepper4Up.disable();
		}
		else if (solutionArray[i].equals("D'")) {
			//stepper2Down.enable();
			stepper2Down.rotate(-90);
			//stepper2Down.disable();
		}

		else if (solutionArray[i].equals("F2")) {
			//stepper6Front.enable();
			stepper6Front.rotate(180);
			//stepper6Front.disable();
		}
		else if (solutionArray[i].equals("B2")) {
			//stepper1Back.enable();
			stepper1Back.rotate(180);
			//stepper1Back.disable();
		}
		else if (solutionArray[i].equals("R2")) {
			//stepper3Right.enable();
			stepper3Right.rotate(180);
			//stepper3Right.disable();
		}
		else if (solutionArray[i].equals("L2")) {
			//stepper5Left.enable();
			stepper5Left.rotate(180);
			//stepper5Left.disable();
		}
		else if (solutionArray[i].equals("U2")) {
			//stepper4Up.enable();
			stepper4Up.rotate(180);
			//stepper4Up.disable();
		}
		else if (solutionArray[i].equals("D2")) {
			//stepper2Down.enable();
			stepper2Down.rotate(180);
			//stepper2Down.disable();
		}
	}
	steppersDisable();
}


void steppersSolveTheCube(String solutionArray[], int i) {
	/*
	steppersEnable();
	if (solutionArray[i].equals("F")) {
		stepper6Front.rotate(90);
	}
	else if (solutionArray[i].equals("B")) {
		stepper1Back.rotate(90);
	}
	else if (solutionArray[i].equals("R")) {
		stepper3Right.rotate(90);
	}
	else if (solutionArray[i].equals("L")) {
		stepper5Left.rotate(90);
	}
	else if (solutionArray[i].equals("U")) {
		stepper4Up.rotate(90);
	}
	else if (solutionArray[i].equals("D")) {
		stepper2Down.rotate(90);
	}

	else if (solutionArray[i].equals("F'")) {
		stepper6Front.rotate(-90);
	}
	else if (solutionArray[i].equals("B'")) {
		stepper1Back.rotate(-90);
	}
	else if (solutionArray[i].equals("R'")) {
		stepper3Right.rotate(-90);
	}
	else if (solutionArray[i].equals("L'")) {
		stepper5Left.rotate(-90);
	}
	else if (solutionArray[i].equals("U'")) {
		stepper4Up.rotate(-90);
	}
	else if (solutionArray[i].equals("D'")) {
		stepper2Down.rotate(-90);
	}

	else if (solutionArray[i].equals("F2")) {
		stepper6Front.rotate(180);
	}
	else if (solutionArray[i].equals("B2")) {
		stepper1Back.rotate(180);
	}
	else if (solutionArray[i].equals("R2")) {
		stepper3Right.rotate(180);
	}
	else if (solutionArray[i].equals("L2")) {
		stepper5Left.rotate(180);
	}
	else if (solutionArray[i].equals("U2")) {
		stepper4Up.rotate(180);
	}
	else if (solutionArray[i].equals("D2")) {
		stepper2Down.rotate(180);
	}
	steppersDisable();*/
	
	if (solutionArray[i].equals("F")) {
		stepper6Front.enable();
		stepper6Front.rotate(90);
		stepper6Front.disable();
	}
	else if (solutionArray[i].equals("B")) {
		stepper1Back.enable();
		stepper1Back.rotate(90);
		stepper1Back.disable();
	}
	else if (solutionArray[i].equals("R")) {
		stepper3Right.enable();
		stepper3Right.rotate(90);
		stepper3Right.disable();
	}
	else if (solutionArray[i].equals("L")) {
		stepper5Left.enable();
		stepper5Left.rotate(90);
		stepper5Left.disable();
	}
	else if (solutionArray[i].equals("U")) {
		stepper4Up.enable();
		stepper4Up.rotate(90);
		stepper4Up.disable();
	}
	else if (solutionArray[i].equals("D")) {
		stepper2Down.enable();
		stepper2Down.rotate(90);
		stepper2Down.disable();
	}

	if (solutionArray[i].equals("F'")) {
		stepper6Front.enable();
		stepper6Front.rotate(-90);
		stepper6Front.disable();
	}
	else if (solutionArray[i].equals("B'")) {
		stepper1Back.enable();
		stepper1Back.rotate(-90);
		stepper1Back.disable();
	}
	else if (solutionArray[i].equals("R'")) {
		stepper3Right.enable();
		stepper3Right.rotate(-90);
		stepper3Right.disable();
	}
	else if (solutionArray[i].equals("L'")) {
		stepper5Left.enable();
		stepper5Left.rotate(-90);
		stepper5Left.disable();
	}
	else if (solutionArray[i].equals("U'")) {
		stepper4Up.enable();
		stepper4Up.rotate(-90);
		stepper4Up.disable();
	}
	else if (solutionArray[i].equals("D'")) {
		stepper2Down.enable();
		stepper2Down.rotate(-90);
		stepper2Down.disable();
	}

	if (solutionArray[i].equals("F2")) {
		stepper6Front.enable();
		stepper6Front.rotate(180);
		stepper6Front.disable();
	}
	else if (solutionArray[i].equals("B2")) {
		stepper1Back.enable();
		stepper1Back.rotate(180);
		stepper1Back.disable();
	}
	else if (solutionArray[i].equals("R2")) {
		stepper3Right.enable();
		stepper3Right.rotate(180);
		stepper3Right.disable();
	}
	else if (solutionArray[i].equals("L2")) {
		stepper5Left.enable();
		stepper5Left.rotate(180);
		stepper5Left.disable();
	}
	else if (solutionArray[i].equals("U2")) {
		stepper4Up.enable();
		stepper4Up.rotate(180);
		stepper4Up.disable();
	}
	else if (solutionArray[i].equals("D2")) {
		stepper2Down.enable();
		stepper2Down.rotate(180);
		stepper2Down.disable();
	}
	
}

void twoSteppersAtOnce(String solutionArray[], int i) {
	//steppersEnable();
	stepper3Right.enable();
	stepper5Left.enable();
	if ((solutionArray[i] == "R" && solutionArray[i + 1] == "L") || (solutionArray[i] == "L" && solutionArray[i + 1] == "R"))
	{
		controllerRightLeft.rotate(90, 90);
	}
	else if ((solutionArray[i] == "R" && solutionArray[i + 1] == "L'") || (solutionArray[i] == "L'" && solutionArray[i + 1] == "R"))
	{
		controllerRightLeft.rotate(90, -90);
	}
	else if ((solutionArray[i] == "R" && solutionArray[i + 1] == "L2") || (solutionArray[i] == "L2" && solutionArray[i + 1] == "R"))
	{
		controllerRightLeft.rotate(90, 180);
	}
	else if ((solutionArray[i] == "R'" && solutionArray[i + 1] == "L") || (solutionArray[i] == "L" && solutionArray[i + 1] == "R'"))
	{
		controllerRightLeft.rotate(-90, 90);
	}
	else if ((solutionArray[i] == "R'" && solutionArray[i + 1] == "L'") || (solutionArray[i] == "L'" && solutionArray[i + 1] == "R'"))
	{
		controllerRightLeft.rotate(-90, -90);
	}
	else if ((solutionArray[i] == "R'" && solutionArray[i + 1] == "L2") || (solutionArray[i] == "L2" && solutionArray[i + 1] == "R'"))
	{
		controllerRightLeft.rotate(-90, 180);
	}
	else if ((solutionArray[i] == "R2" && solutionArray[i + 1] == "L") || (solutionArray[i] == "L" && solutionArray[i + 1] == "R2"))
	{
		controllerRightLeft.rotate(180, 90);
	}
	else if ((solutionArray[i] == "R2" && solutionArray[i + 1] == "L'") || (solutionArray[i] == "L'" && solutionArray[i + 1] == "R2"))
	{
		controllerRightLeft.rotate(180, -90);
	}
	else if ((solutionArray[i] == "R2" && solutionArray[i + 1] == "L2") || (solutionArray[i] == "L2" && solutionArray[i + 1] == "R2"))
	{
		controllerRightLeft.rotate(180, 180);
	}
	stepper3Right.disable();
	stepper5Left.disable();

	stepper1Back.enable();
	stepper6Front.enable();
	if ((solutionArray[i] == "B" && solutionArray[i + 1] == "F") || (solutionArray[i] == "F" && solutionArray[i + 1] == "B"))
	{
		controllerBackFront.rotate(90, 90);
	}
	else if ((solutionArray[i] == "B" && solutionArray[i + 1] == "F'") || (solutionArray[i] == "F'" && solutionArray[i + 1] == "B"))
	{
		controllerBackFront.rotate(90, -90);
	}
	else if ((solutionArray[i] == "B" && solutionArray[i + 1] == "F2") || (solutionArray[i] == "F2" && solutionArray[i + 1] == "B"))
	{
		controllerBackFront.rotate(90, 180);
	}
	else if ((solutionArray[i] == "B'" && solutionArray[i + 1] == "F") || (solutionArray[i] == "F" && solutionArray[i + 1] == "B'"))
	{
		controllerBackFront.rotate(-90, 90);
	}
	else if ((solutionArray[i] == "B'" && solutionArray[i + 1] == "F'") || (solutionArray[i] == "F'" && solutionArray[i + 1] == "B'"))
	{
		controllerBackFront.rotate(-90, -90);
	}
	else if ((solutionArray[i] == "B'" && solutionArray[i + 1] == "F2") || (solutionArray[i] == "F2" && solutionArray[i + 1] == "B'"))
	{
		controllerBackFront.rotate(-90, 180);
	}
	else if ((solutionArray[i] == "B2" && solutionArray[i + 1] == "F") || (solutionArray[i] == "F" && solutionArray[i + 1] == "B2"))
	{
		controllerBackFront.rotate(180, 90);
	}
	else if ((solutionArray[i] == "B2" && solutionArray[i + 1] == "F'") || (solutionArray[i] == "F'" && solutionArray[i + 1] == "B2"))
	{
		controllerBackFront.rotate(180, -90);
	}
	else if ((solutionArray[i] == "B2" && solutionArray[i + 1] == "F2") || (solutionArray[i] == "F2" && solutionArray[i + 1] == "B2"))
	{
		controllerBackFront.rotate(180, 180);
	}
	stepper1Back.disable();
	stepper6Front.disable();

	stepper2Down.enable();
	stepper4Up.enable();
	if ((solutionArray[i] == "D" && solutionArray[i + 1] == "U") || (solutionArray[i] == "U" && solutionArray[i + 1] == "D"))
	{
		controllerDownUp.rotate(90, 90);
	}
	else if ((solutionArray[i] == "D" && solutionArray[i + 1] == "U'") || (solutionArray[i] == "U'" && solutionArray[i + 1] == "D"))
	{
		controllerDownUp.rotate(90, -90);
	}
	else if ((solutionArray[i] == "D" && solutionArray[i + 1] == "U2") || (solutionArray[i] == "U2" && solutionArray[i + 1] == "D"))
	{
		controllerDownUp.rotate(90, 180);
	}
	else if ((solutionArray[i] == "D'" && solutionArray[i + 1] == "U") || (solutionArray[i] == "U" && solutionArray[i + 1] == "D'"))
	{
		controllerDownUp.rotate(-90, 90);
	}
	else if ((solutionArray[i] == "D'" && solutionArray[i + 1] == "U'") || (solutionArray[i] == "U'" && solutionArray[i + 1] == "D'"))
	{
		controllerDownUp.rotate(-90, -90);
	}
	else if ((solutionArray[i] == "D'" && solutionArray[i + 1] == "U2") || (solutionArray[i] == "U2" && solutionArray[i + 1] == "D'"))
	{
		controllerDownUp.rotate(-90, 180);
	}
	else if ((solutionArray[i] == "D2" && solutionArray[i + 1] == "U") || (solutionArray[i] == "U" && solutionArray[i + 1] == "D2"))
	{
		controllerDownUp.rotate(180, 90);
	}
	else if ((solutionArray[i] == "D2" && solutionArray[i + 1] == "U'") || (solutionArray[i] == "U'" && solutionArray[i + 1] == "D2"))
	{
		controllerDownUp.rotate(180, -90);
	}
	else if ((solutionArray[i] == "D2" && solutionArray[i + 1] == "U2") || (solutionArray[i] == "U2" && solutionArray[i + 1] == "D2"))
	{
		controllerDownUp.rotate(180, 180);
	}
	stepper2Down.disable();
	stepper4Up.disable();
	//steppersDisable();
}

int arrayOfSync[400] = {};

void checkArrayAndOptimized(String solutionArray[], int lengthArray) {
	memset(arrayOfSync, 0, sizeof(arrayOfSync));

	String rightFaceArrayCheck[18] = { "R","R","R","R'","R'","R'","R2","R2","R2","L","L","L","L'","L'","L'","L2","L2","L2" };
	String leftFaceArrayCheck[18] = { "L","L'","L2","L","L'","L2","L","L'","L2","R","R'","R2","R","R'","R2" ,"R","R'","R2" };

	String frontFaceArrayCheck[18] = { "F","F","F","F'","F'","F'","F2","F2","F2","B","B","B","B'","B'","B'","B2","B2","B2" };
	String backFaceArrayCheck[18] = { "B","B'","B2","B","B'","B2","B","B'","B2","F","F'","F2","F","F'","F2","F","F'","F2" };

	String upFaceArrayCheck[18] = { "U","U","U","U'","U'","U'","U2","U2","U2","D","D","D","D'","D'","D'","D2","D2","D2" };
	String downFaceArrayCheck[18] = { "D","D'","D2","D","D'","D2","D","D'","D2","U","U'","U2","U","U'","U2","U","U'","U2" };

	for (int i = 1; i <= lengthArray ; i++) {
		for (int j = 0; j < 18; j++)
		{
			if (solutionArray[i-1].equals(rightFaceArrayCheck[j]) && solutionArray[i].equals(leftFaceArrayCheck[j]))
			{
				arrayOfSync[i - 1] = 0;
				arrayOfSync[i] = 1;
				i++;
				break;
			}
			else if (solutionArray[i-1].equals(frontFaceArrayCheck[j]) && solutionArray[i].equals(backFaceArrayCheck[j]))
			{
				arrayOfSync[i - 1] = 0;
				arrayOfSync[i] = 1;
				i++;
				break;
			}
			else if (solutionArray[i-1].equals(upFaceArrayCheck[j]) && solutionArray[i].equals(downFaceArrayCheck[j]))
			{
				arrayOfSync[i - 1] = 0;
				arrayOfSync[i] = 1;
				i++;
				break;
			}
			else {
				arrayOfSync[i-1] = 2;
			}
		}
		
	}
	/*
	for (int i = 0; i < lengthArray; i++) {
		Serial.print("\r\n Array [" + (String)i + "] : " + solutionArray[i] + " - ArrayOfSync ["+ (String)arrayOfSync[i]+"]");
	}
	*/
}



void steppersSolveTheCubeOptimized(String solutionArray[], int lengthArray) {
	checkArrayAndOptimized(solutionArray, lengthArray);
	
	for (int i = 0; i < lengthArray; i++) {
		if (arrayOfSync[i] == 0)
		{
			twoSteppersAtOnce(solutionArray, i);
		}
		else if (arrayOfSync[i] == 2)
		{
			steppersSolveTheCube(solutionArray, i);
		}
	}
	
}

void changeSteppersRPM(int speedRPM) {
	stepper1Back.begin(speedRPM, MICROSTEPS);
	stepper2Down.begin(speedRPM, MICROSTEPS);
	stepper3Right.begin(speedRPM, MICROSTEPS);
	stepper4Up.begin(speedRPM, MICROSTEPS);
	stepper5Left.begin(speedRPM, MICROSTEPS);
	stepper6Front.begin(speedRPM, MICROSTEPS);

	steppersDisable();
}

void steppersEnable() {
	stepper1Back.enable();
	stepper2Down.enable();
	stepper3Right.enable();
	stepper4Up.enable();
	stepper5Left.enable();
	stepper6Front.enable();
}
void steppersDisable() {
	stepper1Back.disable();
	stepper2Down.disable();
	stepper3Right.disable();
	stepper4Up.disable();
	stepper5Left.disable();
	stepper6Front.disable();
}

void testPattern2() {
	stepper6Front.rotate(90);
	stepper1Back.rotate(90);
	stepper3Right.rotate(90);
	stepper5Left.rotate(90);
	stepper4Up.rotate(90);
	stepper2Down.rotate(90);
}

void gripTheCube() {
	stepperDown.setSpeedProfile(stepperDown.LINEAR_SPEED, MOTOR_ACCEL, 2000);

	if (chosenMode == 0)
	{
		stepperUp.enable();
		stepperDown.enable();
		controllerGripRelease.rotate(170, -170);
		stepperUp.disable();
		/*
		stepperUp.enable();
		stepperUp.rotate(-180);
		stepperUp.disable();

		stepperDown.enable();
		stepperDown.rotate(170);
		*/
		//stepperDown.disable();
		
	}
	//turnOnOffLeds = true;
	onOffDownRing = false;
}
void releaseTheCube() {
	stepperDown.setSpeedProfile(stepperDown.LINEAR_SPEED, MOTOR_ACCEL, MOTOR_DECEL);
	if (chosenMode == 0 || chosenMode == 2)
	{
		stepperDown.enable();
		stepperUp.enable();
		controllerGripRelease.rotate(-170, 170);
		stepperDown.disable();
		/*
		stepperDown.enable();
		stepperDown.rotate(-170);
		stepperDown.disable();
		stepperUp.enable();
		stepperUp.rotate(180);
		*/
	}
	//turnOnOffLeds = false;
	onOffDownRing = false;
}

void stringArrayToCharArray(String stringToProcces, String cornersEdges) {
	//Serial.println("StringTo Procces: "+stringToProcces);
	if (cornersEdges.equals("corners")) {
		memset(bldCornersArray, 0, sizeof(bldCornersArray));
		stringToProcces.toCharArray(bldCornersArray, 60);
	}
	else if (cornersEdges.equals("edges"))
	{
		
		memset(bldEdgesArray, 0, sizeof(bldEdgesArray));
		stringToProcces.toCharArray(bldEdgesArray, 60);
	}
	/*Serial.print("\r\n----");
	for (int i = 0; i < KmbBldMoves[1]; i++)
	{
		Serial.print(bldCornersArray[i] + ",");
	}*/
	
	bldPartialToFinalArray(bldCornersArray, bldEdgesArray);

	//Serial.print("\r\n Corners final String:"+ cornersFinalString+".");

	if (KmbBldMoves[1] % 2 == 0 || KmbBldMoves[2] % 2 == 0)
	{
		parityCheck = false;
	}
	else
	{
		parityCheck = true;
	}
	//Serial.print("\r\n KmBmoves[1]: " + (String)KmbBldMoves[1] + " KmBmoves[2]: " + (String)KmbBldMoves[2]+ " parity Check: "+ (String)parityCheck+"\r\n");

}

void bldPartialToFinalArray(char cornersArray[], char edgesArray[]) {
	cornersFinalString = "";
	edgesFinalString = "";

	for (int i = 0; i < 60; i++)
	{
		switch (cornersArray[i])
		{
		case 'B':
			cornersFinalString += cornerB + " " + oldPochmannAlgorithm + " " + cornerB2b + " ";
			break;
		case 'C':
			cornersFinalString += cornerC + " " + oldPochmannAlgorithm + " " + cornerC2b + " ";
			break;
		case 'D':
			cornersFinalString += cornerD + " " + oldPochmannAlgorithm + " " + cornerD2b + " ";
			break;
		case 'F':
			cornersFinalString += cornerF + " " + oldPochmannAlgorithm + " " + cornerF + " ";
			break;
		case 'G':
			cornersFinalString += cornerG + " " + oldPochmannAlgorithm + " " + cornerG2b + " ";
			break;
		case 'H':
			cornersFinalString += cornerH + " " + oldPochmannAlgorithm + " " + cornerH + " ";
			break;
		case 'I':
			cornersFinalString += "F' D " + oldPochmannAlgorithm + " " + "D' F" + " ";
			break;
		case 'J':
			cornersFinalString += cornerJ + " " + oldPochmannAlgorithm + " " + cornerJ2b + " ";
			break;
		case 'K':
			cornersFinalString += cornerK + " " + oldPochmannAlgorithm + " " + cornerK2b + " ";
			break;
		case 'L':
			cornersFinalString += cornerL + " " + oldPochmannAlgorithm + " " + cornerL2b + " ";
			break;
		case 'M':
			cornersFinalString += cornerM + " " + oldPochmannAlgorithm + " " + cornerM2b + " ";
			break;
		case 'N':
			cornersFinalString += cornerN + " " + oldPochmannAlgorithm + " " + cornerN + " ";
			break;
		case 'O':
			cornersFinalString += cornerO + " " + oldPochmannAlgorithm + " " + cornerO2b + " ";
			break;
		case 'P':
			cornersFinalString += cornerP + " " + oldPochmannAlgorithm + " " + cornerP2b + " ";
			break;
		case 'Q':
			cornersFinalString += cornerQ + " " + oldPochmannAlgorithm + " " + cornerQ2b + " ";
			break;
		case 'S':
			cornersFinalString += cornerS + " " + oldPochmannAlgorithm + " " + cornerS2b + " ";
			break;
		case 'T':
			cornersFinalString += cornerT + " " + oldPochmannAlgorithm + " " + cornerT2b + " ";
			break;
		case 'U':
			cornersFinalString += cornerU + " " + oldPochmannAlgorithm + " " + cornerU2b + " ";
			break;
		case 'V':
			cornersFinalString += cornerV + " " + oldPochmannAlgorithm + " " + cornerV2b + " ";
			break;
		case 'W':
			cornersFinalString += cornerW + " " + oldPochmannAlgorithm + " " + cornerW2b + " ";
			break;
		case 'X':
			cornersFinalString += cornerX + " " + oldPochmannAlgorithm + " " + cornerX2b + " ";
			break;
		default:
			break;
		}
	}

	for (int i = 0; i < 60; i++)
	{
		if (i % 2 == 0)
		{
			switch (edgesArray[i])
			{
			case 'A':
				edgesFinalString += letterA + " ";
				break;
			case 'B':
				edgesFinalString += letterB + " ";
				break;
			case 'C':
				edgesFinalString += letterC + " ";
				break;
			case 'D':
				edgesFinalString += letterD + " ";
				break;
			case 'E':
				edgesFinalString += letterE + " ";
				break;
			case 'F':
				edgesFinalString += letterF + " ";
				break;
			case 'G':
				edgesFinalString += letterG + " ";
				break;
			case 'H':
				edgesFinalString += letterH + " ";
				break;
			case 'I':
				edgesFinalString += letterI + " ";
				break;
			case 'J':
				edgesFinalString += letterJ + " ";
				break;
			case 'L':
				edgesFinalString += letterL + " ";
				break;
			case 'M':
				edgesFinalString += letterM + " ";
				break;
			case 'N':
				edgesFinalString += letterN + " ";
				break;
			case 'O':
				edgesFinalString += letterO + " ";
				break;
			case 'P':
				edgesFinalString += letterP + " ";
				break;
			case 'Q':
				edgesFinalString += letterQ + " ";
				break;
			case 'R':
				edgesFinalString += letterR + " ";
				break;
			case 'S':
				edgesFinalString += letterS + " ";
				break;
			case 'T':
				edgesFinalString += letterT + " ";
				break;
			case 'V':
				edgesFinalString += letterV + " ";
				break;
			case 'W':
				edgesFinalString += letterW + " ";
				break;
			case 'X':
				edgesFinalString += letterX + " ";
				break;
			default:
				break;
			}
		}
		else
		{
			switch (edgesArray[i])
			{
			case 'A':
				edgesFinalString += letterA + " ";
				break;
			case 'B':
				edgesFinalString += letterB2m + " ";
				break;
			case 'C':
				edgesFinalString += letterC2m + " ";
				break;
			case 'D':
				edgesFinalString += letterD2m + " ";
				break;
			case 'E':
				edgesFinalString += letterE2m + " ";
				break;
			case 'F':
				edgesFinalString += letterF2m + " ";
				break;
			case 'G':
				edgesFinalString += letterG2m + " ";
				break;
			case 'H':
				edgesFinalString += letterH2m + " ";
				break;
			case 'I':
				edgesFinalString += letterI2m + " ";
				break;
			case 'J':
				edgesFinalString += letterJ2m + " ";
				break;
			case 'L':
				edgesFinalString += letterL2m + " ";
				break;
			case 'M':
				edgesFinalString += letterM2m + " ";
				break;
			case 'N':
				edgesFinalString += letterN2m + " ";
				break;
			case 'O':
				edgesFinalString += letterO2m + " ";
				break;
			case 'P':
				edgesFinalString += letterP2m + " ";
				break;
			case 'Q':
				edgesFinalString += letterQ2m + " ";
				break;
			case 'R':
				edgesFinalString += letterR2m + " ";
				break;
			case 'S':
				edgesFinalString += letterS2m + " ";
				break;
			case 'T':
				edgesFinalString += letterT2m + " ";
				break;
			case 'V':
				edgesFinalString += letterV2m + " ";
				break;
			case 'W':
				edgesFinalString += letterW2m + " ";
				break;
			case 'X':
				edgesFinalString += letterX2m + " ";
				break;
			default:
				break;
			}
		}
	}
}


void bldCombinedSteppers() {
	sumAll = 0;

	if (parityCheck == true) {
		sumAll = 8 + KmbBldMoves[3] + KmbBldMoves[4];

		for (int i = 0; i < KmbBldMoves[4]; i++) {
			blindfoldedCombinedArrayForSteppers[i] = blindfoldedEdgesArrayForSteppers[i];
		}
		for (int i = KmbBldMoves[4]; i < KmbBldMoves[4] + 8; i++) {
			blindfoldedCombinedArrayForSteppers[i] = parityArray[i - KmbBldMoves[4]];
		}
		for (int i = KmbBldMoves[4] + 8; i < sumAll; i++) {
			blindfoldedCombinedArrayForSteppers[i] = blindfoldedCornersArrayForSteppers[i - (KmbBldMoves[4] + 8)];
		}
	}
	else if (parityCheck == false)
	{
		sumAll = KmbBldMoves[3] + KmbBldMoves[4];
		for (int i = 0; i < KmbBldMoves[4]; i++) {
			blindfoldedCombinedArrayForSteppers[i] = blindfoldedEdgesArrayForSteppers[i];
		}

		for (int i = KmbBldMoves[4]; i < sumAll; i++) {
			blindfoldedCombinedArrayForSteppers[i] = blindfoldedCornersArrayForSteppers[i - KmbBldMoves[4]];
		}
	}
	/*Serial.println();
	for (int i = 0; i < sumAll; i++)
	{
		Serial.print(blindfoldedCombinedArrayForSteppers[i]+",");
	}*/
}


void showOnDisplayAndSwichDot() {
	switch (switchDot)
	{
	case 'c':
		pickNumberAndDigit((n / 10000), D1);
		pickNumberAndDigit(((n % 10000) / 1000), D2);
		pickNumberAndDigit(((n % 1000) / 100), D3);
		pickNumberAndDigit(((n % 100) / 10), D4);
		dotOnDigit(D2);

		break;
	case 'd':
		pickNumberAndDigit((n / 1000), D1);
		pickNumberAndDigit(((n % 1000) / 100), D2);
		pickNumberAndDigit(((n % 100) / 10), D3);
		pickNumberAndDigit((n % 10), D4);
		dotOnDigit(D1);
		break;
	default:
		pickNumberAndDigit((n / 1000), D1);
		pickNumberAndDigit(((n % 1000) / 100), D2);
		pickNumberAndDigit(((n % 100) / 10), D3);
		pickNumberAndDigit((n % 10), D4);
		dotOnDigit(D1);
		break;
	}
}


void pickNumberAndDigit(int x, int digit)
{
	switch (x)
	{
	default:
		zero(digit);
		break;
	case 1:
		one(digit);
		break;
	case 2:
		two(digit);
		break;
	case 3:
		three(digit);
		break;
	case 4:
		four(digit);
		break;
	case 5:
		five(digit);
		break;
	case 6:
		six(digit);
		break;
	case 7:
		seven(digit);
		break;
	case 8:
		eight(digit);
		break;
	case 9:
		nine(digit);
		break;
	}
}

void clearLEDs(int digitOnDisplay)
{
	digitalWrite(latchPin, LOW);
	shiftOut(dataPin, clockPin, MSBFIRST, digitOnDisplay >> 8);
	shiftOut(dataPin, clockPin, MSBFIRST, digitOnDisplay);
	digitalWrite(latchPin, HIGH);
}

void dotOnDigit(int digitOnDisplay)
{
	digitalWrite(latchPin, LOW);
	shiftOut(dataPin, clockPin, MSBFIRST, digitOnDisplay + 128 >> 8);
	shiftOut(dataPin, clockPin, MSBFIRST, digitOnDisplay + 128);
	digitalWrite(latchPin, HIGH);
}

void zero(int digitOnDisplay)
{
	digitalWrite(latchPin, LOW);
	shiftOut(dataPin, clockPin, MSBFIRST, digitOnDisplay + 63 >> 8);
	shiftOut(dataPin, clockPin, MSBFIRST, digitOnDisplay + 63);
	digitalWrite(latchPin, HIGH);
}

void one(int digitOnDisplay)
{
	digitalWrite(latchPin, LOW);
	shiftOut(dataPin, clockPin, MSBFIRST, digitOnDisplay + 6 >> 8);
	shiftOut(dataPin, clockPin, MSBFIRST, digitOnDisplay + 6);
	digitalWrite(latchPin, HIGH);
}

void two(int digitOnDisplay)
{
	digitalWrite(latchPin, LOW);
	shiftOut(dataPin, clockPin, MSBFIRST, digitOnDisplay + 91 >> 8);
	shiftOut(dataPin, clockPin, MSBFIRST, digitOnDisplay + 91);
	digitalWrite(latchPin, HIGH);
}

void three(int digitOnDisplay)
{
	digitalWrite(latchPin, LOW);
	shiftOut(dataPin, clockPin, MSBFIRST, digitOnDisplay + 79 >> 8);
	shiftOut(dataPin, clockPin, MSBFIRST, digitOnDisplay + 79);
	digitalWrite(latchPin, HIGH);
}

void four(int digitOnDisplay)
{
	digitalWrite(latchPin, LOW);
	shiftOut(dataPin, clockPin, MSBFIRST, digitOnDisplay + 102 >> 8);
	shiftOut(dataPin, clockPin, MSBFIRST, digitOnDisplay + 102);
	digitalWrite(latchPin, HIGH);
}

void five(int digitOnDisplay)
{
	digitalWrite(latchPin, LOW);
	shiftOut(dataPin, clockPin, MSBFIRST, digitOnDisplay + 109 >> 8);
	shiftOut(dataPin, clockPin, MSBFIRST, digitOnDisplay + 109);
	digitalWrite(latchPin, HIGH);
}

void six(int digitOnDisplay)
{
	digitalWrite(latchPin, LOW);
	shiftOut(dataPin, clockPin, MSBFIRST, digitOnDisplay + 125 >> 8);
	shiftOut(dataPin, clockPin, MSBFIRST, digitOnDisplay + 125);
	digitalWrite(latchPin, HIGH);
}

void seven(int digitOnDisplay)
{
	digitalWrite(latchPin, LOW);
	shiftOut(dataPin, clockPin, MSBFIRST, digitOnDisplay + 7 >> 8);
	shiftOut(dataPin, clockPin, MSBFIRST, digitOnDisplay + 7);
	digitalWrite(latchPin, HIGH);
}

void eight(int digitOnDisplay)
{
	digitalWrite(latchPin, LOW);
	shiftOut(dataPin, clockPin, MSBFIRST, digitOnDisplay + 127 >> 8);
	shiftOut(dataPin, clockPin, MSBFIRST, digitOnDisplay + 127);
	digitalWrite(latchPin, HIGH);
}

void nine(int digitOnDisplay)
{
	digitalWrite(latchPin, LOW);
	shiftOut(dataPin, clockPin, MSBFIRST, digitOnDisplay + 103 >> 8);
	shiftOut(dataPin, clockPin, MSBFIRST, digitOnDisplay + 103);
	digitalWrite(latchPin, HIGH);
}

void add()
{
	countForShow++;
	if (countForShow == 5) {
		countForShow = 0;
	}

	if (startStopTimer == true)
	{
		count++;
	}
	if (count == 10)
	{
		count = 0;
		n++;

	}
	if (n == 10000)
	{
		switchDot = 'c';
	}
}

void resetTimer() {
	count = 0;
	n = 0;
	switchDot = 'a';
}
