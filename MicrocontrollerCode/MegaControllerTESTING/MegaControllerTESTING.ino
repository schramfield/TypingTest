// No more spikershields! Let's fucking do it.
//
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

//SIDE SPECIFIC VARIABLES ////////////////////////////////////////////////////
//LEFT side
int Li = 0; // i will be our STIM value, it goes 1-12, each step representing 5kohms
//int Lsustain = 0;
int Lmaximum = 0;
bool calibL = false;
bool Ltouch = false;

//RIGHT side
int Ri = 0; // i will be our STIM value, it goes 1-12, each step representing 5kohms
//int Rsustain = 0;
int Rmaximum = 0;
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



  digitalWrite(lights[0], HIGH);
  delay(500);
  for(int q = 0; q < 10; q++){
    digitalWrite(lights[q], HIGH);
    digitalWrite(lights[q-1], LOW);
    delay(flsh);
  }
  digitalWrite(r2, LOW);

  rainbow();  
  
  }
// LOOP PHASE ////////////////////////////////////
void loop(){ ///////////////////////////////////////

  if (digitalRead(STIMup) == HIGH) {
    digitalWrite(g1, HIGH);
  }
  else if (digitalRead(STIMup) == LOW) {
    digitalWrite(g1, LOW);
  }
  if(digitalRead(STIMdown) == HIGH){
    digitalWrite(g2, HIGH);
  }
  else if(digitalRead(STIMdown) == LOW){
    digitalWrite(g2, LOW);
  }

  if (digitalRead(SetCalib) == HIGH) {
    digitalWrite(y1, HIGH);
  }
  else if (digitalRead(SetCalib) == LOW) {
    digitalWrite(y1, LOW);
  }
  if (digitalRead(LCalib) == HIGH) {
    digitalWrite(r1, HIGH);
  }
  else if (digitalRead(LCalib) == LOW) {
    digitalWrite(r1, LOW);
  }
  if(digitalRead(RCalib) == HIGH) {
    digitalWrite(r2, HIGH);
  }
  else if (digitalRead(RCalib) == LOW) {
    digitalWrite(r2, LOW);
  }
}

bool rainbow() {
  for(int j = 0; j < 7; j++) {
    digitalWrite(STIMlights[j], HIGH);
    delay(flsh);
    digitalWrite(STIMlights[j], LOW);
  }
}

// END OF CODE /////////////////////////////
