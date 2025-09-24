read -p "Enter a fruit name (apple, banana, orange): " fruit

case $fruit in
    apple)
        echo "You chose apple."
        ;;
    banana)
        echo "You chose banana."
        ;;
    orange)
        echo "You chose orange."
        ;;
    *)
        echo "Unknown fruit."
        ;;
esac

