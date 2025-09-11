#include "C:\Users\PC\.vscode\Repos\Basic\Embedded Systems\LED_HAL_Project\LED_Button_HAl\HAL\hal_gpio.c"
#include "C:\Users\PC\.vscode\Repos\Basic\Embedded Systems\LED_HAL_Project\LED_Button_HAl\HAL\hal_gpio.h"
 
int main(void) {
    GPIO_Init(); // Initialize GPIO settings

    while (1) {
        GPIO_Update(); // Update LED state based on button press
    }

    return 0; // This line will never be reached
}