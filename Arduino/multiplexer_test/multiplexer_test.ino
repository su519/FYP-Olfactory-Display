#define A 11
#define B 12
#define C 13
#define Y1 8
#define Y2 9
#define Y3 10


int count = 0;

void setup() {
  pinMode(A, OUTPUT);
  pinMode(B, OUTPUT);
  pinMode(C, OUTPUT);
  pinMode(Y1, INPUT);
  pinMode(Y2, INPUT);
  pinMode(Y3, INPUT);
  pinMode(17, OUTPUT);
  digitalWrite(17, LOW);
  Serial.begin(9600);


}

void loop() {
  unsigned long binary = 0;
  char binaryStr[17];

    
  for (int add = 0; add <= 7; add = add + 1) {
    digitalWrite(A, (add & 0b001) >> 0);
    digitalWrite(B, (add & 0b010) >> 1);
    digitalWrite(C, (add & 0b100) >> 2);
  
    binary = (binary << 1) | digitalRead(Y1);
    delay(1);
  }

  for (int add = 0; add <= 7; add = add + 1) {
    digitalWrite(A, (add & 0b001) >> 0);
    digitalWrite(B, (add & 0b010) >> 1);
    digitalWrite(C, (add & 0b100) >> 2);
    
  
    binary = (binary << 1) | digitalRead(Y2);
    delay(1);
    
  }

  for (int add = 0; add <= 1; add = add + 1) {
    digitalWrite(A, (add & 0b001) >> 0);
    digitalWrite(B, (add & 0b010) >> 1);
    digitalWrite(C, (add & 0b100) >> 2);
    
  
    binary = (binary << 1) | digitalRead(Y3);
    delay(1);
    
  }

  for (int i = 17; i >= 0; i--) {
  binaryStr[17 - i] = ((binary >> i) & 0x01) + '0';
  }

//  binaryStr[18] = '\0'; // Null-terminate the string

  Serial.println(binaryStr);


}
