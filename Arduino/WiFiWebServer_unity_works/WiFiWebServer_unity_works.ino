#include <WiFiNINA.h>

char ssid[] = "Selin";
char pass[] = "selinuygun";
int status = WL_IDLE_STATUS;

WiFiServer server(80);
bool start = false;
bool scent_code = false;

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
            // byte message1[] = {0x30, 0x30, 0x30, 0x31}; // Message "0001" as binary data
            // client.write(message1, sizeof(message1));
            // Serial.write(message1, sizeof(message1));
            // delay(100);
            // byte message2[] = {0x30, 0x30, 0x31, 0x30}; // Message "0010" as binary data
            // client.write(message2, sizeof(message2));
            // Serial.write(message2, sizeof(message2));
            //000 001 010 011 100 101
            char binary[] = "000001010011100101";

            // Convert binary to string
            // String binaryString = String(binary);

            // Create a byte array to hold the string data
            byte message[sizeof(binary)];

            // Copy the string characters to the byte array
            for (int i = 0; i < sizeof(binary); i++) {
              message[i] = binary[i];
            }

            // Send the byte array to the client
            client.write(message, sizeof(message));
            Serial.write(message, sizeof(message));

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
            if (c >= '2' && c <= '9') {
              int pin = c - '0'; // convert the character to an integer
              if (client.available()) {
                char next_c = client.read();
                if (next_c == '0' || next_c == '1' || next_c == '2') {
                  int value = next_c - '0'; 
                  Serial.print("Pin ");
                  Serial.print(pin);
                  Serial.print(" set to ");
                  Serial.println(value);
                  digitalWrite(pin, value);
                  break;
                }
              }
            }else if( c == 'x'){
              client.stop();
              for (int j = 2; j <= 9; j++) {
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
