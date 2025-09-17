function Button_component(){
    const buttonhandler=()=>{
        console.log("Hello World");
    }
    return(
        <>
            <button onClick={buttonhandler}>Click me</button>
        
        </>

    );
}

export default Button_component;