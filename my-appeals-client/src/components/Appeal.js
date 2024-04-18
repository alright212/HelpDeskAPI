import React from 'react';

function Appeal({appeal, markAsDone}) {
    // Ensure the style uses the color directly from the appeal.color
    const style = {color: appeal.color.includes("red") ? "red" : "black"};

    return (
        <div style={style}>
            <h2>{appeal.description}</h2>
            <p>Deadline: {new Date(appeal.deadlineDate).toLocaleDateString()}</p>
            {!appeal.isDone && (
                <button onClick={() => markAsDone(appeal.id)}>Mark as Done</button>
            )}
        </div>
    );
}

export default Appeal;
