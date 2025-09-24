read -p "Enter the marks: " num

if [[ "$num" -ge 0 && "$num" -lt 25 ]]; then
    echo "You are in first bucket"
elif [[ "$num" -ge 25 && "$num" -le 50 ]]; then
    echo "You are in second bucket"
else
    echo "You are in third bucket"
fi

