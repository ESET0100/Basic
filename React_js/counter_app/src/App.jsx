import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import './App.css'

function counter(){
  //console.log(x);
  var a=5; 
  //console.log(x)
  var b=10
  //console.log(x);
  var add=(a,b)=>a+b;
  
  console.log(add(a,b));

//destructuring props
  const user={name:"John",age:30};
  const{name,age}=user;
  console.log(name);
  console.log(age);

//spread operator
  const prev={name:"John",age:30};
  const next={...prev,age:31};
  console.log(next);

//rest operator (destructing arrays)
  const arr1=[1,2,3];
  const arr2=[4,5,6];
  const arr3=[...arr1,...arr2];
  console.log(arr3);

//interpolation
  const greeting="Hello";
  const place="World";
  console.log(`${greeting}, ${place}!`);

//object shorthand
  const x=10;
  const y=20;
  const point={x,y};
  console.log(point);
  console.log(point.x,point.y);

//method shorthand
  const obj={
    greet(){
      console.log("Hello");
    }
  };
  obj.greet();

//map function
  const items=[1,2,3];
  const doubled=items.map(item=>item*2);
  console.log(doubled);

//filter function
  const numbers=[1,2,3,4,5];
  const even=numbers.filter(num=>num%2===0);
  console.log(even);

//reduce function
  const nums=[1,2,3,4];
  const sum=nums.reduce((acc,curr)=>acc+curr,1);
  console.log(sum);
  // maximum accumulator
  const max=nums.reduce((acc,curr)=>acc>curr?acc:curr);
  console.log(max);



  return (
    <div>
    <h1 id='cntr'>0</h1>
    <button onClick={() => {
      const element=document.getElementById('cntr');
      var curr=parseInt(element.textContent);
      element.textContent=curr+1;
    }}>Increment</button>

    <button onClick={() => {
      const element=document.getElementById('cntr');
      var curr=parseInt(element.textContent);
      element.textContent=curr-1;
    }}>Decrement</button>
    </div>
  )
}

// function App() {
//   const [count, setCount] = useState(0)

//   return (
//     <>
//       <div>
//         <a href="https://vite.dev" target="_blank">
//           <img src={viteLogo} className="logo" alt="Vite logo" />
//         </a>
//         <a href="https://react.dev" target="_blank">
//           <img src={reactLogo} className="logo react" alt="React logo" />
//         </a>
//       </div>
//       <h1>Vite + React</h1>
//       <div className="card">
//         <button onClick={() => setCount((count) => count + 1)}>
//           count is {count}
//         </button>
//         <p>
//           Edit <code>src/App.jsx</code> and save to test HMR
//         </p>
//       </div>
//       <p className="read-the-docs">
//         Click on the Vite and React logos to learn more
//       </p>
//     </>
//   )
// }

// export default App
export default counter
