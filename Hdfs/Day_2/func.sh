# Function to add two numbers
add_numbers() {
    local num1=$1
    local num2=$2
    local sum=$((num1 + num2))
    echo "$sum"
}

# Calling the function with two numbers
result=$(add_numbers 5 7)
echo "The sum is: $result"

