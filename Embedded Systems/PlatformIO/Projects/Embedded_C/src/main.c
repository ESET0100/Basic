//#include <Arduino.h>
/*#include <stdio.h>

// put function declarations here:
int myFunction(int, int);

void setup() {
  // put your setup code here, to run once:
  int result = myFunction(2, 3);
}

void loop() {
  // put your main code here, to run repeatedly:
}

// put function definitions here:
int myFunction(int x, int y) {
  return x + y;
}
  */

#include <avr/io.h>
#include <util/delay.h>

#define LED_PIN PD4

int main(void){
  DDRD |= (1 << LED_PIN); // Set LED_PIN as output
  while(1){
    //Turn LED on
    PORTD |= (1 << LED_PIN); 
    _delay_ms(500); // Wait for 1 second
    //Turn LED off    
    PORTD &= ~(1 << LED_PIN); 
    _delay_ms(500); // Wait for 1 second
  }
}

