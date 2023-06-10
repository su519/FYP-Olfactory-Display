//define iress pins of the DEMUX
#define A 11 
#define B 12
#define C 13

//define output pins of the DEMUX
#define Y1 8
#define Y2 9
#define Y3 10

//define enable pin of the DEMUX
#define enable 17


int count = 0;

void setup() {

  //set the pin modes of the Arduino pins
  //Arduino will write to the iress pins and read from the output pins 
  pinMode(A, OUTPUT);
  pinMode(B, OUTPUT);
  pinMode(C, OUTPUT);
  pinMode(Y1, INPUT);
  pinMode(Y2, INPUT);
  pinMode(Y3, INPUT);
  pinMode(enable, OUTPUT);
  Serial.begin(9600);


}

void loop() {
  //set the enable pin to LOW to enable the DEMUX
  digitalWrite(enable, LOW);

  unsigned long binary = 0;
  char binaryStr[17];

  //itirate through the address pins of the first DEMUX from 000 to 111
  for (int i = 0; i <= 7; i = i + 1) {
    digitalWrite(A, (i & 0b001) >> 0);
    digitalWrite(B, (i & 0b010) >> 1);
    digitalWrite(C, (i & 0b100) >> 2);

    //store the output in the bonary long
    binary = (binary << 1) | digitalRead(Y1);
    delay(1);
  }

  //itirate through the address pins of the second DEMUX from 000 to 111
  for (int i = 0; i <= 7; i = i + 1) {
    digitalWrite(A, (i & 0b001) >> 0);
    digitalWrite(B, (i & 0b010) >> 1);
    digitalWrite(C, (i & 0b100) >> 2);
    
    //add the new output to the end of the binary long
    binary = (binary << 1) | digitalRead(Y2);
    delay(1);
    
  }

  //itirate through the address pins of the lasr DEMUX from 000 to 111
  for (int i = 0; i <= 1; i = i + 1) {
    digitalWrite(A, (i & 0b001) >> 0);
    digitalWrite(B, (i & 0b010) >> 1);
    digitalWrite(C, (i & 0b100) >> 2);
    
    //add the new output to the end of the binary long
    binary = (binary << 1) | digitalRead(Y3);
    delay(1);
    
  }

  //convert long type into string
  for (int i = 17; i >= 0; i--) {
  binaryStr[17 - i] = ((binary >> i) & 0x01) + '0';
  }

  Serial.println(binaryStr);


}
