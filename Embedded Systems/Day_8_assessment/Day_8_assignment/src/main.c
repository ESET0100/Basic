#include <avr/io.h>
#include <util/delay.h>
#include <Arduino.h>
 
#define LED_PIN PD7 // PD4
#define BUT_PIN PD2 // PD2
 
// Helper: read button (returns 1 if pressed, 0 if not)
uint8_t button_pressed() {
  return !(PIND & (1 << BUT_PIN));
}
 
// LED control functions
void led_on() {
  PORTD |= (1 << LED_PIN);
}
void led_off() {
  PORTD &= ~(1 << LED_PIN);
}
 
// Now the main function to initialize and run the modes
 
void setup() {
  // Set PD4 as O/P
  DDRD |= (1 << LED_PIN);
  // Set PD2 as I/P
  DDRD &= ~(1 << BUT_PIN);
  PORTD |= (1 << BUT_PIN); // Enable pull-up on PD2
  led_off(); // Ensure LED is off initially
}
 
void loop() {
  static uint8_t mode = 0; // 0: off, 1: slow, 2: fast, 3: on
  static uint8_t prev_button_state = 0; // 1: pressed, 0: not pressed
 
  uint8_t curr_button_state = button_pressed();
 
  // Detect rising edge: button was not pressed, now pressed
  if (!prev_button_state && curr_button_state) {
    mode++;
    if (mode > 3) mode = 1;
    _delay_ms(200); // Debounce delay
  }
  prev_button_state = curr_button_state;
 
  switch (mode) {
    case 0: // LED off
      led_off();
      break;
    case 1: // Slow blink
      led_on();
      _delay_ms(500);
      led_off();
      _delay_ms(500);
      break;
    case 2: // Fast blink
      led_on();
      _delay_ms(100);
      led_off();
      _delay_ms(100);
      break;
    case 3: // LED ON
      led_on();
      _delay_ms(20); // Short delay to allow button polling
      break;
  }
}
 
int main(void){
  setup();
  while (1) {
    loop();
  }
  return 0;
}
 
 