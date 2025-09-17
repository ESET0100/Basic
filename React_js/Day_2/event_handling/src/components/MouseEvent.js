function MouseEvent(){
    return(
        <>
            <div style={{background:"black",color:"red",padding:"50px"}}
            onMouseEnter={()=>(console.log("mouse enter div"))}
            >Welcome to mouse events</div>
        </>
    );
}

export default MouseEvent;