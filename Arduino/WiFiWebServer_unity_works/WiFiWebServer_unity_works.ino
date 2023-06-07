#include <WiFiNINA.h>
#define fan 14
#define A 11
#define B 12
#define C 13
#define Y1 8
#define Y2 9
#define Y3 10
#define atom 18

char ssid[] = "Selin";
char pass[] = "selinuygun";
int status = WL_IDLE_STATUS;

WiFiServer server(80);
bool start = false;
bool study = false;
bool scent_code = false;
char str_pin[2] = {};
int prev_c = 0;
int user = 0;
char c1;
char c2;

void setup() {
  Serial.begin(9600);
  
  while (status != WL_CONNECTED) {
    Serial.print("Attempting to connect to SSID: ");
    Serial.println(ssid);
    status = WiFi.begin(ssid, pass);
    delay(1000);
  }
  
  Serial.print("Connected to WiFi network with IP Address: ");
  Serial.println(WiFi.localIP());
  
  server.begin();
  pinMode(2,OUTPUT);
  pinMode(3,OUTPUT);
  pinMode(4,OUTPUT);
  pinMode(5,OUTPUT);
  pinMode(6,OUTPUT);
  pinMode(7,OUTPUT);
  pinMode(8,OUTPUT);
  pinMode(9,OUTPUT);
  pinMode(atom,OUTPUT);
  pinMode(fan, OUTPUT);
  pinMode(A, OUTPUT);
  pinMode(B, OUTPUT);
  pinMode(C, OUTPUT);
  pinMode(Y1, INPUT);
  pinMode(Y2, INPUT);
  pinMode(Y3, INPUT);
  pinMode(17, OUTPUT);
  digitalWrite(17, LOW);
}

void loop() {
  WiFiClient client = server.available();
  if(client){
    while (client.connected()) {
      if (client.available()) {
        char c = client.read();
        // Serial.write(c);
        if(c == 'l'){
            delay(100);
            unsigned long binary = 0;
            char binarymessage[18];

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
              binarymessage[17 - i] = ((binary >> i) & 0x01) + '0';
            }
            
//            char binary[] = "000001010011100101";

            byte message[sizeof(binarymessage)];

            for (int i = 0; i < sizeof(binarymessage); i++) {
              message[i] = binarymessage[i];
            }

            client.write(message, sizeof(message));
//            Serial.write(message, sizeof(message));

            scent_code = true;
            delay(100);
            break;


        }
        if(scent_code == true){
          if(c == 'u'){
            study = true;
            break;
          }
          if(c == 's'){
            start = true;
            break;
          }
          if(start == true){
            // char c = client.read();
            if (c >= '1' && c <= '9') {
              int pin = c - '0'; // convert the character to an integer
//              Serial.print("prev_c: ");
//              Serial.println(prev_c);
//              Serial.print("pin: ");
//              Serial.println(pin);
//              Serial.print(c);
              if (client.available()) {
                c1 = client.read();
//                Serial.println(c1);
                if (c1 == 'i' || c1 == 'o' || (c1 >= '0' && c1 <= '9')) {
                  if (c1 >= '0' && c1 <= '9'){
                    if (client.available()) {
                      c2 = client.read();
                      str_pin[0] = c; 
                      str_pin[1] = c1; 
                      pin = atoi(str_pin);
                      if(c2 == 'i'){
//                        Serial.print("Pin ");
//                        Serial.print(pin);
//                        Serial.print(" set to ");
//                        Serial.println(1);
                        digitalWrite(pin, 1);
                        break;
                      }else if(c2 == 'o'){
//                        Serial.print("Pin ");
//                        Serial.print(pin);
//                        Serial.print(" set to ");
//                        Serial.println(0);
                        digitalWrite(pin, 0);
                        break;
                      }
                    }
                  }else if (c1 == 'i'){
//                    Serial.print("Pin ");
//                    Serial.print(pin);
//                    Serial.print(" set to ");
//                    Serial.println(1);
                    if (c >= '2' && c <= '7'){
                      digitalWrite(atom, 1);
                    }
                    digitalWrite(pin, 1);
                    prev_c = pin;
                    break;
                  }else if(c1 == 'o'){
//                    Serial.print("Pin ");
//                    Serial.print(pin);
//                    Serial.print(" set to ");
//                    Serial.println(0);
                    if (c >= '2' && c <= '7'){
                      digitalWrite(atom, 0);
                    }
                    digitalWrite(pin, 0);
                    prev_c = pin;
                    break;
                  }
                  break;
                }
              }
              
            }else if( c == 'x'){
              client.stop();
              for (int j = 2; j <= 14; j++) {
                  digitalWrite(j, 0);
              }
              start = false;
              study = false;
              break;
            }else if(c == '0'){
//              Serial.print(c);
              String detect = "00";
              if (client.available()) {
                c1 = client.read();
                if(c1 == '0'){
                  while(client.available()){
                    c2 = client.read();
                    detect = detect + c2;
                  }
                  Serial.println(detect);
                  break;
                }else{
                  if (client.available()) {
                    c2 = client.read();
    //                  Serial.println(c2);
                  }
                }
              }
              
              if(c1 != '0'){
                int scentPin = c1 - '0';
                int userPin = c2 - '0';
                if(study == true){
                  log2csv(scentPin, userPin);
                  break;
                }
              }
              break;
            }

          }
        }
      }
    }
  }
}

void log2csv(int pin, int user){
  Serial.print(pin);
  Serial.print(',');
  Serial.println(user);
}
