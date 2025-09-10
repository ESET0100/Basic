#include <Arduino.h>
#include <avr/io.h>
#include <util/delay.h>

#define DATA_PIND6 PB0
#define DATA_PIND5 PB1
#define DATA_PIND4 PB2
#define DATA_PIND7 PD7
#define ENABLE_PIN PB3
#define DATA_RS PB4

void lcd_init(){
    _delay_ms(200); // Wait for LCD to power up
    lcd_command(0x02); // Initialize LCD in 4-bit mode
    lcd_command(0x28); // 2 lines, 5x7 matrix
    lcd_command(0x0C); // Display ON, Cursor OFF
    lcd_command(0x06); // Increment cursor  
    lcd_command(0x01); // Clear display
    _delay_ms(200); // Wait for clear display command to complete
  
}

void lcd_print(const char *str){
    while(*str){
        lcd_data(*str++);
    }
}



void lcd_pulse(){
    PORTB |= (1<<ENABLE_PIN); // Enable high
    _delay_us(1); // Wait for 1 microsecond
    PORTB &= ~(1<<ENABLE_PIN); // Enable low
    _delay_us(100); // Wait for command to process

}

void lcd_command(unsigned char cmd){
    PORTB &= ~(1<<DATA_RS); // RS = 0 for command
    // Send higher nibble
    if(cmd & 0x10){
        PORTB |= (1<<DATA_PIND4);
    } else {
        PORTB &= ~(1<<DATA_PIND4);
    }
    if(cmd & 0x20) PORTB |= (1<<DATA_PIND5); else PORTB &= ~(1<<DATA_PIND5);
    if(cmd & 0x40) PORTB |= (1<<DATA_PIND6); else PORTB &= ~(1<<DATA_PIND6);
    if(cmd & 0x80) PORTD |= (1<<DATA_PIND7); else PORTD &= ~(1<<DATA_PIND7);
    lcd_pulse();

    // Send lower nibble
    if(cmd & 0x01) PORTB |= (1<<DATA_PIND4); else PORTB &= ~(1<<DATA_PIND4);
    if(cmd & 0x02) PORTB |= (1<<DATA_PIND5  ); else PORTB &= ~(1<<DATA_PIND5);
    if(cmd & 0x04) PORTB |= (1<<DATA_PIND6  ); else PORTB &= ~(1<<DATA_PIND6);
    if(cmd & 0x08) PORTD |= (1<<DATA_PIND7); else PORTD &= ~(1<<DATA_PIND7);
    lcd_pulse();

  
}

void lcd_data(unsigned char data){
    PORTB |= (1<<DATA_RS); // RS = 1 for data
    // Send higher nibble
    if(data & 0x10) PORTB |= (1<<DATA_PIND4); else PORTB &= ~(1<<DATA_PIND4);
    if(data & 0x20) PORTB |= (1<<DATA_PIND5); else PORTB &= ~(1<<DATA_PIND5);
    if(data & 0x40) PORTB |= (1<<DATA_PIND6); else PORTB &= ~(1<<DATA_PIND6);
    if(data & 0x80) PORTD |= (1<<DATA_PIND7); else PORTD &= ~(1<<DATA_PIND7);
    lcd_pulse();
    // Send lower nibble
    if(data & 0x01) PORTB |= (1<<DATA_PIND4 ); else PORTB &= ~(1<<DATA_PIND4);
    if(data & 0x02) PORTB |= (1<<DATA_PIND5 ); else PORTB &= ~(1<<DATA_PIND5);
    if(data & 0x04) PORTB |= (1<<DATA_PIND6 ); else PORTB &= ~(1<<DATA_PIND6);
    if(data & 0x08) PORTD |= (1<<DATA_PIND7); else PORTD &= ~(1<<DATA_PIND7);
    lcd_pulse();

  
}

int main(void){
    DDRB = 0xFF; // Set PORTB as output
    DDRD|= (1<<DATA_PIND7); // Set PD7 as output
    lcd_init();

    lcd_command(0x80); // Move cursor to first line, first position
    lcd_print("Hi Lakshay :)"); // Print message

    lcd_command(0xC0); // Move cursor to second line, first position
    lcd_print("Good Morning!!"); // Print message

    while (1) 
    {
    }
}




