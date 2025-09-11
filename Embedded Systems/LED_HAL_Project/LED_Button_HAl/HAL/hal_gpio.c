#include "hal_gpio.h"

void GPIO_Init(void) {
    // Set LED_PIN as output
    DDRD |= (1 << LED_PIN);
   
    // Set BUTTON_PIN as input
    DDRD &= ~(1 << BUTTON_PIN);
   
    // Enable pull-up resistor on BUTTON_PIN
    PORTD |= (1 << BUTTON_PIN);
}
void GPIO_Update(void) {
    // Check if button is pressed
    if (!(PIND & (1 << BUTTON_PIN))) {
        // Turn LED on
        PORTD |= (1 << LED_PIN);
    } else {
        // Turn LED off
        PORTD &= ~(1 << LED_PIN);
    }
}

