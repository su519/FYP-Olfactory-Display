#define A 11
#define B 12
#define C 13
#define i1 8
#define i2 9


int count = 0;

void setup() {
  pinMode(A, OUTPUT);
  pinMode(B, OUTPUT);
  pinMode(C, OUTPUT);
  pinMode(i1, INPUT);
  pinMode(i2, INPUT);
  pinMode(17, OUTPUT);
  digitalWrite(17, LOW);
  Serial.begin(9600);


}

void loop() {
  unsigned long binary = 0;

  for (int add = 0; add <= 7; add = add + 1) {
    digitalWrite(A, (add & 0b001) >> 0);
    digitalWrite(B, (add & 0b010) >> 1);
    digitalWrite(C, (add & 0b100) >> 2);
    
  
    binary = (binary << 1) | digitalRead(i2);
    delay(1);
    
  }
  
  for (int add = 0; add <= 7; add = add + 1) {
    digitalWrite(A, (add & 0b001) >> 0);
    digitalWrite(B, (add & 0b010) >> 1);
    digitalWrite(C, (add & 0b100) >> 2);
  
    binary = (binary << 1) | digitalRead(i1);
    delay(1);
  }
  
  for (int i = 15; i >= 0; i = i - 1) {
    Serial.print((binary >> i) & 0x01);
  }
  Serial.println();

}
