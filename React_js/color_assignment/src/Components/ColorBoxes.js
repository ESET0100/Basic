import React, { useRef, useState } from "react";

function ColorBoxes() {
  const boxRefs = [useRef(null), useRef(null), useRef(null)];
  const [highlighted, setHighlighted] = useState(-1);

  const colors = ["red", "green", "blue"];

  const handleHighlight = () => {
    // Remove highlight from previous box
    if (highlighted !== -1) {
      boxRefs[highlighted].current.style.border = "none";
    }
    // Calculate next box index
    const next = (highlighted + 1) % boxRefs.length;
    // Highlight next box
    boxRefs[next].current.style.border = "4px solid yellow";
    setHighlighted(next);
  };

  return (
    <div>
      <div style={{ display: "flex", gap: "20px", marginBottom: "20px" }}>
        {colors.map((color, idx) => (
          <div
            key={color}
            ref={boxRefs[idx]}
            style={{
              width: "100px",
              height: "100px",
              background: color,
              borderRadius: "8px",
              transition: "border 0.2s"
            }}
          />
        ))}
      </div>
      <button onClick={handleHighlight}>Highlight Next Box</button>
    </div>
  );
}

export default ColorBoxes;