#define A 8
#define B 9
#define C 10
#define i1 7
#define i2 6

int count = 0;
void setup() {
  pinMode(A, OUTPUT);
  pinMode(B, OUTPUT);
  pinMode(C, OUTPUT);
  pinMode(i1, INPUT);
  pinMode(i2, INPUT);
//  digitalWrite(vapo,HIGH);

  Serial.begin(9600);
  
}

void loop() {
  if(count == 0){
    digitalWrite(A,0);
    digitalWrite(B,0);
    digitalWrite(C,0);
    Serial.print(digitalRead(i1));
    delay(200);
    digitalWrite(A,1);
    digitalWrite(B,0);
    digitalWrite(C,0);
    Serial.print(digitalRead(i1));
    delay(200);
    digitalWrite(A,0);
    digitalWrite(B,1);
    digitalWrite(C,0);
    Serial.print(digitalRead(i1));
    delay(200);
    digitalWrite(A,1);
    digitalWrite(B,1);
    digitalWrite(C,0);
    Serial.print(digitalRead(i1));
    delay(200);
    digitalWrite(A,0);
    digitalWrite(B,0);
    digitalWrite(C,1);
    Serial.print(digitalRead(i1));
    delay(200);
    digitalWrite(A,1);
    digitalWrite(B,0);
    digitalWrite(C,1);
    Serial.print(digitalRead(i1));
    delay(200);
    digitalWrite(A,0);
    digitalWrite(B,1);
    digitalWrite(C,1);
    Serial.print(digitalRead(i1));
    delay(200);
    digitalWrite(A,1);
    digitalWrite(B,1);
    digitalWrite(C,1);
    Serial.print(digitalRead(i1));
    delay(200);   
    
    digitalWrite(A,0);
    digitalWrite(B,0);
    digitalWrite(C,0);
    Serial.print(digitalRead(i2));
    delay(200);
    digitalWrite(A,1);
    digitalWrite(B,0);
    digitalWrite(C,0);
    Serial.print(digitalRead(i2));
    delay(200);
    digitalWrite(A,0);
    digitalWrite(B,1);
    digitalWrite(C,0);
    Serial.print(digitalRead(i2));
    delay(200);
    digitalWrite(A,1);
    digitalWrite(B,1);
    digitalWrite(C,0);
    Serial.print(digitalRead(i2));
    delay(200);
    digitalWrite(A,0);
    digitalWrite(B,0);
    digitalWrite(C,1);
    Serial.print(digitalRead(i2));
    delay(200);
    digitalWrite(A,1);
    digitalWrite(B,0);
    digitalWrite(C,1);
    Serial.print(digitalRead(i2));
    delay(200);
    digitalWrite(A,0);
    digitalWrite(B,1);
    digitalWrite(C,1);
    Serial.print(digitalRead(i2));
    delay(200);
    digitalWrite(A,1);
    digitalWrite(B,1);
    digitalWrite(C,1);
    Serial.print(digitalRead(i2));
    delay(200);
    count = 1;
  }

  
}
