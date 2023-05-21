#include <WiFiNINA.h>
#define fan 14

char ssid[] = "Selin";
char pass[] = "selinuygun";
int status = WL_IDLE_STATUS;

WiFiServer server(80);
bool start = false;
bool scent_code = false;
char str_pin[2] = {};

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
  pinMode(fan, OUTPUT);
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
            char binary[] = "000001010011100101";

            byte message[sizeof(binary)];

            for (int i = 0; i < sizeof(binary); i++) {
              message[i] = binary[i];
            }

            client.write(message, sizeof(message));
            // Serial.write(message, sizeof(message));

            scent_code = true;
            delay(100);
            break;


        }
        if(scent_code == true){
          if(c == 's'){
            start = true;
            break;
          }
          if(start == true){
            // char c = client.read();
            if (c >= '1' && c <= '9') {
              int pin = c - '0'; // convert the character to an integer
              Serial.println(pin);
              if (client.available()) {
                char c1 = client.read();
                if (c1 == 'i' || c1 == 'o' || (c1 >= '0' && c1 <= '9')) {
                  if (c1 >= '0' && c1 <= '9'){
                    if (client.available()) {
                      char c2 = client.read();
                      str_pin[0] = c; 
                      str_pin[1] = c1; 
                      pin = atoi(str_pin);

                      if(c2 == 'i'){
                        Serial.print("Pin ");
                        Serial.print(pin);
                        Serial.print(" set to ");
                        Serial.println(1);
                        digitalWrite(pin, 1);
                        break;
                      }else if(c2 == 'o'){
                        Serial.print("Pin ");
                        Serial.print(pin);
                        Serial.print(" set to ");
                        Serial.println(0);
                        digitalWrite(pin, 0);
                        break;
                      }
                    }
                  }else if (c1 == 'i'){
                    Serial.print("Pin ");
                    Serial.print(pin);
                    Serial.print(" set to ");
                    Serial.println(1);
                    digitalWrite(pin, 1);
                    break;
                  }else if(c1 == 'o'){
                    Serial.print("Pin ");
                    Serial.print(pin);
                    Serial.print(" set to ");
                    Serial.println(0);
                    digitalWrite(pin, 0);
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
              break;
            }  
          }
        }
      }
    }
  }
}
