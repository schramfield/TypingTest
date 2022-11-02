// trying to convert serial communication to ints/bytes

#include <SoftwareSerial.h>

// Defining all this beautiful nonsense in case I need to change the way I wire the real board
#define LCalib 30 // Switch to calibrate LEFT
#define RCalib  28 // Switch to calibrate RIGHT
#define SetCalib 26 // Green button sets it
#define STIMup 24 // nice up arrow
#define STIMdown 22 // down arrow

//LIGHTS blue/green/yellow/red ////////////
#define b1 42
#define b2 52
#define g1 40
#define g2 50
#define g3 36 // will indicate that LEFT hand is calibrated
#define g4 46 // will indicate that RIGHT hand is calibrated
#define y1 38
#define y2 48
#define r1 34
#define r2 44
int lights[10] = {b1, b2, g1, g2, g3, g4, y1, y2, r1, r2};

//RELAYS //////////////////
//LEFT relays
#define LNC 53
#define LNO 51
#define Lohm5 49
#define Lohm10 47
#define Lohm20 45
#define Lohm25 43
int LEFTside[4] = {Lohm5, Lohm10, Lohm20, Lohm25};

//RIGHT relays
#define RNC 23
#define RNO 25
#define Rohm5 27
#define Rohm10 29
#define Rohm20 31
#define Rohm25 33
int RIGHTside[4] = {Rohm5, Rohm10, Rohm20, Rohm25};

int STIMlights[6] = {g1, g2, y1, y2, r1, r2};

//useful numbers maybe //////////////////////////////
#define flsh 75 // this way I can change the flash time easily
#define tap 100 // let's work out what feels natural...
#define rest 250
#define pause 500

// Unity-related variables
bool receivingSerial = false;

//SIDE SPECIFIC VARIABLES ////////////////////////////////////////////////////
//LEFT side
int Li = 0; // i will be our STIM value, it goes 1-12, each step representing 5kohms
int Lmaximum = 0;
int Lsustain = 0;
bool calibL = false;
bool Ltouch = false;

//RIGHT side
int Ri = 0; // i will be our STIM value, it goes 1-12, each step representing 5kohms
int Rmaximum = 0;
int Rsustain = 0;
bool calibR = false;
bool Rtouch = false;

// SETUP PHASE ///////////////////////////////////////
void setup() //////////////////////////////////////////
{
  // set all the lights
  pinMode(b1, OUTPUT); // Indicates it's on
  pinMode(b2, OUTPUT); // indicates calibration mode
  pinMode(g1, OUTPUT);
  pinMode(g2, OUTPUT);
  pinMode(y1, OUTPUT);  
  pinMode(y2, OUTPUT);
  pinMode(r1, OUTPUT);
  pinMode(r2, OUTPUT);
  pinMode(g3, OUTPUT); // LEFT hand calibrated
  pinMode(g4, OUTPUT); // RIGHT hand calibrated

  // set all the relays
  pinMode(LNC, OUTPUT);  //normally closed, HIGH = off
  pinMode(LNO, OUTPUT);  // normally open, HIGH = on
  pinMode(Lohm5, OUTPUT); // 5kohms
  pinMode(Lohm10, OUTPUT); // 10kohms
  pinMode(Lohm20, OUTPUT); // 20kohms
  pinMode(Lohm25, OUTPUT); // 25 kohms

  pinMode(RNC, OUTPUT);
  pinMode(RNO, OUTPUT);
  pinMode(Rohm5, OUTPUT); // 5kohms
  pinMode(Rohm10, OUTPUT); // 10kohms
  pinMode(Rohm20, OUTPUT); // 20kohms
  pinMode(Rohm25, OUTPUT); // 25 kohms


  // start at max resistance, shut off w/safetys, and flash the lights a couple of times
  //
  set60(LEFTside);
  set60(RIGHTside);
  digitalWrite(LNC, HIGH);
  digitalWrite(LNO, LOW);
  digitalWrite(RNC, HIGH);
  digitalWrite(RNO, LOW);
  digitalWrite(b1, HIGH);
  flashALL();
  delay(flsh);
  rainbow();
  delay(flsh);
  flashALL();

  // get the serial port working
  Serial.begin(9600);  
}
// LOOP PHASE ////////////////////////////////////
void loop(){ ///////////////////////////////////////

  // Check for calibration ////////////////
  //
  if (digitalRead(LCalib) == LOW && calibL == true){
    calibL = false;
    updateI(0, LEFTside);
    delay(pause);
  }
  if (digitalRead(RCalib) == LOW && calibR == true){
    calibR = false;
    updateI(0, RIGHTside);
    delay(pause);
  }
  if(calibL == false && calibR == false && receivingSerial == false) {
    digitalWrite(b2, LOW);
  }
   
  // update STIM if calibrating /////////////////
  //
  if (calibL == true) {
    updateI(Li, LEFTside);
  }
  if (calibR == true) {
    updateI(Ri, RIGHTside);
  }
  
  // Take commands from UNITY ////////////////////
  // LEFT = 1, RIGHT = 2, LEFT & RIGHT = 3, nothing = 0
  //
  if (calibL == false && calibR == false) {
    
    // LEFT == 1
    if (fromUnity() == 1) {
      updateI(Lmaximum, LEFTside);
      delay(tap);
      // debugger lights
      //
      //flash2(g1,g2);
      }
      
    // RIGHT == 2
    else if (fromUnity() == 2){
      updateI(Rmaximum, RIGHTside);
       delay(tap);
      // debugger lights
      //
      //rainbow();
      }
      
    // BOTH == 3
    else if (fromUnity() == 3){
      updateI(Rmaximum, RIGHTside);
      updateI(Lmaximum, LEFTside);
      delay(tap);
      // debugger lights
      //
      //flash2(y1, y2);
    }
    
    // nothing == 0 or nothing
    else if (fromUnity() == 0){
      updateI(0, LEFTside);
      updateI(0, RIGHTside);
      delay(tap);
      // debugger lights
      //
      //flash2(r1, r2);
    }
  }


  //////// Going into Calibration mode ////////////////////
  //
  // begin calibrate LEFT
  //
  if (digitalRead(LCalib) == HIGH && calibL == false) {
    calibL = true;
    digitalWrite(b2, HIGH);
    digitalWrite(LNC, LOW);
    digitalWrite(LNO, HIGH);
    Li = 0;
    Lmaximum = 0;
    allOFF();
    flash(g3);
    delay(flsh);
    flash(g3);
    delay(flsh);
  }

  // begin calibrate RIGHT
  //
  if (digitalRead(RCalib) == HIGH && calibR == false) {
    calibR = true;
    digitalWrite(b2, HIGH);
    digitalWrite(RNC, LOW);
    digitalWrite(RNO, HIGH);
    Ri = 0;
    Rmaximum = 0;
    allOFF();
    flash(g4);
    delay(flsh);
    flash(g4);
    delay(flsh);
  }

  // Increase LEFT
  //
  if (digitalRead(STIMup) == HIGH && calibL == true && Li < 12) {
    Li = Li+1;
    wink(b2);
    delay(pause);
  }

  // Increase RIGHT
  //
    if (digitalRead(STIMup) == HIGH && calibR == true && Ri < 12) {
    Ri = Ri+1;
    wink(b2);
    delay(pause);
  }

  // Decrease LEFT
  //
  if (digitalRead(STIMdown) == HIGH && calibL == true && Li > -1) {
    Li = Li-1;
    wink(b2);
    delay(pause);
  }

  // Decrease RIGHT
  //
    if (digitalRead(STIMdown) == HIGH && calibR == true && Ri > -1) {
    Ri = Ri-1;
    wink(b2);
    delay(pause);
  }

  // Set LEFT
  //
  if (digitalRead(SetCalib) == HIGH && calibL == true) {
    Lmaximum = Li;
    rainbow();
    allOFF();
    digitalWrite(g3, HIGH);
    delay(rest);
  }

  // Set RIGHT
  //
  if (digitalRead(SetCalib) == HIGH && calibR == true) {
    Rmaximum = Ri;
    rainbow();
    allOFF();
    digitalWrite(g4, HIGH);
    delay(rest);
  }
}
// END OF LOOP

// FUNCTIONS //////////////////////////////////
// LIGHT RELATED FUNCTIONS //////////////////////
//
bool flash(int LED) {
  digitalWrite(LED, HIGH);
  delay(flsh);
  digitalWrite(LED, LOW);
  return true;
}
bool flash2(int LED1, int LED2) {
  digitalWrite(LED1, HIGH);
  digitalWrite(LED2, HIGH);
  delay(flsh);
  digitalWrite(LED1, LOW);
  digitalWrite(LED2, LOW);
  return true;
}
bool flashALL() {
  digitalWrite(g1, HIGH);
  digitalWrite(g2, HIGH);
  digitalWrite(y1, HIGH);
  digitalWrite(y2, HIGH);
  digitalWrite(r1, HIGH);
  digitalWrite(r2, HIGH);
  delay(flsh);
  digitalWrite(g1, LOW);
  digitalWrite(g2, LOW);
  digitalWrite(y1, LOW);
  digitalWrite(y2, LOW);
  digitalWrite(r1, LOW);
  digitalWrite(r2, LOW);
}
bool rainbow() {
  for(int j = 0; j < 7; j++) {
    digitalWrite(STIMlights[j], HIGH);
    delay(flsh);
    digitalWrite(STIMlights[j], LOW);
  }
}
bool wink (int LED) {
  digitalWrite(LED, LOW);
  delay(flsh);
  digitalWrite(LED, HIGH);
  return true;
}
bool allOFF() {
  digitalWrite(g1, LOW);
  digitalWrite(g2, LOW);
  digitalWrite(y1, LOW);
  digitalWrite(y2, LOW);
  digitalWrite(r1, LOW);
  digitalWrite(r2, LOW);
}


// UNITY RELATED FUNCTIONS ///////////////////////
int fromUnity(){
    if(Serial.available()){
    receivingSerial = true;
    digitalWrite(b2, HIGH);
    digitalWrite(b1, LOW);
    
    // should be char maybe????
    //
    int data = Serial.read();

    if (data = 0) {
      return 0;
    }
    if(data = 1){
      return 1;
    }
    if(data = 2){
      return 2;
    }
    if(data = 3) {
      return 3;
    }
  }
  else {
    receivingSerial = false;
    digitalWrite(b2, LOW);
    digitalWrite(b1, HIGH);
  }
  return -1;
}

// RESISTANCE RELATED FUNCTIONS //////////////////////
bool updateI(int i, int* SIDE) {
  if (i <= 0) { // 60 kOhms
  set60(SIDE);
  }
  if (i == 1) { // 55 kOhms
   set55(SIDE);
   }
  if (i == 2) { // 50 kOhms
    set50(SIDE);
  }
  if (i == 3) { // 45 kOhms
    set45(SIDE);
  }
  if (i == 4) { // 40 kOhms
    set40(SIDE);
  }
  if (i == 5) { // 35 kOhms
    set35(SIDE);
  }
  if (i == 6) { // 30 kOhms
    set30(SIDE);
  }
  if (i == 7) { // 25 kOhms
    set25(SIDE);
  }
  if (i == 8) { // 20 kOhms
    set20(SIDE);
  }
  if (i == 9) { // 15 kOhms
    set15(SIDE);
  }
  if (i == 10) { // 10 kOhms
    set10(SIDE);
  }
  if(i == 11) { // 5 kOHMS
    set5(SIDE);
  }
  if(i >= 12) { // 0 kOHMS
    set0(SIDE);
  }  
  return true;
}

bool set60(int* ohm) {
  // set the resistance level
  digitalWrite(ohm[0], LOW);
  digitalWrite(ohm[1], LOW);
  digitalWrite(ohm[2], LOW);
  digitalWrite(ohm[3], LOW);
  // set the lights
  digitalWrite(g1, LOW);
  digitalWrite(g2, LOW);
  digitalWrite(y1, LOW);
  digitalWrite(y2, LOW);
  digitalWrite(r1, LOW);
  digitalWrite(r2, LOW);    
  return true;
}
bool set55(int* ohm) {
  digitalWrite(ohm[0], HIGH);
  digitalWrite(ohm[1], LOW);
  digitalWrite(ohm[2], LOW);
  digitalWrite(ohm[3], LOW); 
  // set the lights
  digitalWrite(g1, LOW);
  digitalWrite(g2, LOW);
  digitalWrite(y1, LOW);
  digitalWrite(y2, LOW);
  digitalWrite(r1, LOW);
  digitalWrite(r2, LOW);  
  return true;
}
bool set50(int* ohm) {
  digitalWrite(ohm[0], LOW);
  digitalWrite(ohm[1], HIGH);
  digitalWrite(ohm[2], LOW);
  digitalWrite(ohm[3], LOW);
  // set the lights
  digitalWrite(g1, HIGH);
  digitalWrite(g2, LOW);
  digitalWrite(y1, LOW);
  digitalWrite(y2, LOW);
  digitalWrite(r1, LOW);
  digitalWrite(r2, LOW);
  return true;
}
bool set45(int* ohm){
  digitalWrite(ohm[0], HIGH);
  digitalWrite(ohm[1], HIGH);
  digitalWrite(ohm[2], LOW);
  digitalWrite(ohm[3], LOW);
  // set the lights
  digitalWrite(g1, HIGH);
  digitalWrite(g2, LOW);
  digitalWrite(y1, LOW);
  digitalWrite(y2, LOW);
  digitalWrite(r1, LOW);
  digitalWrite(r2, LOW);
  return true;
}
bool set40(int* ohm) {
  digitalWrite(ohm[0], LOW);
  digitalWrite(ohm[1], LOW);
  digitalWrite(ohm[2], HIGH);
  digitalWrite(ohm[3], LOW);
  // set the lights
  digitalWrite(g1, HIGH);
  digitalWrite(g2, HIGH);
  digitalWrite(y1, LOW);
  digitalWrite(y2, LOW);
  digitalWrite(r1, LOW);
  digitalWrite(r2, LOW);
  return true;
}
bool set35(int* ohm) {
  digitalWrite(ohm[0], HIGH);
  digitalWrite(ohm[1], LOW);
  digitalWrite(ohm[2], HIGH);
  digitalWrite(ohm[3], LOW);
  // set the lights
  digitalWrite(g1, HIGH);
  digitalWrite(g2, HIGH);
  digitalWrite(y1, LOW);
  digitalWrite(y2, LOW);
  digitalWrite(r1, LOW);
  digitalWrite(r2, LOW);
  return true;
}
bool set30(int* ohm) {
  digitalWrite(ohm[0], HIGH);
  digitalWrite(ohm[1], LOW);
  digitalWrite(ohm[2], LOW);
  digitalWrite(ohm[3], HIGH);
  // set the lights
  digitalWrite(g1, HIGH);
  digitalWrite(g2, HIGH);
  digitalWrite(y1, HIGH);
  digitalWrite(y2, LOW);
  digitalWrite(r1, LOW);
  digitalWrite(r2, LOW);
  return true;
}
bool set25(int* ohm) {
  digitalWrite(ohm[0], HIGH);
  digitalWrite(ohm[1], HIGH);
  digitalWrite(ohm[2], HIGH);
  digitalWrite(ohm[3], LOW);
  // set the lights
  digitalWrite(g1, HIGH);
  digitalWrite(g2, HIGH);
  digitalWrite(y1, HIGH);
  digitalWrite(y2, LOW);
  digitalWrite(r1, LOW);
  digitalWrite(r2, LOW);
  return true;
}
bool set20(int* ohm) {
  digitalWrite(ohm[0], HIGH);
  digitalWrite(ohm[1], HIGH);
  digitalWrite(ohm[2], LOW);
  digitalWrite(ohm[3], HIGH);
  // set the lights
  digitalWrite(g1, HIGH);
  digitalWrite(g2, HIGH);
  digitalWrite(y1, HIGH);
  digitalWrite(y2, HIGH);
  digitalWrite(r1, LOW);
  digitalWrite(r2, LOW);
  return true;
}
bool set15(int* ohm) {
  digitalWrite(ohm[0], LOW);
  digitalWrite(ohm[1], LOW);
  digitalWrite(ohm[2], HIGH);
  digitalWrite(ohm[3], HIGH);
  // set the lights
  digitalWrite(g1, HIGH);
  digitalWrite(g2, HIGH);
  digitalWrite(y1, HIGH);
  digitalWrite(y2, HIGH);
  digitalWrite(r1, LOW);
  digitalWrite(r2, LOW);
  return true;
}
bool set10(int* ohm) {
  digitalWrite(ohm[0], HIGH);
  digitalWrite(ohm[1], LOW);
  digitalWrite(ohm[2], HIGH);
  digitalWrite(ohm[3], HIGH);
  // set the lights
  digitalWrite(g1, HIGH);
  digitalWrite(g2, HIGH);
  digitalWrite(y1, HIGH);
  digitalWrite(y2, HIGH);
  digitalWrite(r1, HIGH);
  digitalWrite(r2, LOW);
  return true;
}
bool set5(int* ohm) {
  digitalWrite(ohm[0], LOW);
  digitalWrite(ohm[1], HIGH);
  digitalWrite(ohm[2], HIGH);
  digitalWrite(ohm[3], HIGH);
  // set the lights
  digitalWrite(g1, HIGH);
  digitalWrite(g2, HIGH);
  digitalWrite(y1, HIGH);
  digitalWrite(y2, HIGH);
  digitalWrite(r1, HIGH);
  digitalWrite(r2, LOW);  
  return true;
}
bool set0(int* ohm) { //MAX STIM
  digitalWrite(ohm[0], HIGH);
  digitalWrite(ohm[1], HIGH);
  digitalWrite(ohm[2], HIGH);
  digitalWrite(ohm[3], HIGH);
 // set the lights
  digitalWrite(g1, HIGH);
  digitalWrite(g2, HIGH);
  digitalWrite(y1, HIGH);
  digitalWrite(y2, HIGH);
  digitalWrite(r1, HIGH);
  digitalWrite(r2, HIGH);
  return true;
}  

// END OF CODE /////////////////////////////
