import { useEffect } from "react";
import { useState } from "react";
 
function UseEffectComponent(){
 
    const suggestions = [
        "maxhub", "laptop", "bottle", "car", "bike", "mouse"
    ];
 
    const [recomendation, setRecomendation] = useState([]);
 
    const [search, setSearch] = useState("");
 
    useEffect(()=>{
        if(search===""){
            setRecomendation(suggestions);
        }
        else{
            setRecomendation(suggestions.filter((element)=>element.includes(search)));
            console.log("Action called!");
        }
       
    }, [search]);
 
    return (
        <div>
            <input value = {search} onChange = {(e)=>setSearch(e.target.value)} />
            {
                recomendation.map((rec, index)=>(<div key={index}>{index+1}. {rec}</div>))
            }
        </div>
    );
}
 
export default UseEffectComponent;