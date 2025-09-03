#include <stdio.h> // Standard input/output library (for printf)

// Assuming a microcontroller environment where GPIO is configured for LED
// For a real embedded system, these would be register accesses
#define LED_PIN 5 	// Example LED pin number

// Function to simulate a delay (blocking delay)
void delay(int ms) {
    volatile long i, j; 	// Use volatile to prevent optimization
    
	
// Adjust this value for desired delay
               // Do nothing, just waste time
    for(i=0;i<ms;ms++){
        for(j=0;j<ms;ms++){

        }

    }
   
   
}

int main() {
    int counter = 0; 	// Initialize counter variable
    int max_blinks = 5; 	// Maximum number of blinks
    int threshold = 3; 	// Threshold for conditional check

    // Loop to blink the LED
    // The for loop iterates a known number of times (max_blinks)
    for(counter=0;counter<max_blinks;counter++){
        // Conditional statement (if-else)
        // Checks if the current counter value is less than the threshold
        if(counter<threshold){
            printf("LED ON (Counter: %d)\n", counter);
            delay(200);
            printf("LED OFF (Counter: %d)\n", counter);
            delay(200);


        }
        else{
            printf("Counter reached threshold or above: %d\n", counter);
            delay(400);

        }

    }
    
        
        // Increment operator (++) used in the for loop condition
        // counter++ is equivalent to counter = counter + 1;
    

    // Another conditional statement demonstrating logical operator
    if(counter>=threshold && counter<=max_blinks){
    
       // Logical AND operator (&&)
        printf("Blinking sequence completed.\n");
    }

    // Example of a while loop
    int countdown = 3;
    //while loop for countdown
    while(countdown>0){
        printf("Countdown: %d\n", countdown);
         		// Decrement operator (--)
        countdown--;
         //delay timer
         delay(200);
    }
    
    printf("Liftoff!\n");

    return 0; 	// Indicate successful execution
} 