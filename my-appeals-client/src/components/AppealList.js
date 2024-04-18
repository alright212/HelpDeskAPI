import React from 'react';
import Appeal from './Appeal';

function AppealsList({appeals, markAsDone}) {
    return (
        <ul className="appeals-list">
            {appeals.map(appeal => (
                <li key={appeal.id} className="appeal">
                    <Appeal appeal={appeal} markAsDone={markAsDone}/>
                </li>
            ))}
        </ul>
    );
}

export default AppealsList;
