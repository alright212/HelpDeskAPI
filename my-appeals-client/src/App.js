import React, {useState, useEffect} from 'react';
import AppealsList from "./components/AppealList";
import AddAppeal from './components/AddAppeal';
import axios from 'axios';


function App() {
    const [appeals, setAppeals] = useState([]);

    useEffect(() => {
        const fetchAppeals = async () => {
            try {
                const response = await axios.get('http://localhost:5244/api/appeals');
                setAppeals(response.data);
            } catch (error) {
                console.error('Error fetching data: ', error);
            }
        };

        fetchAppeals();
    }, []);

    const addAppealToState = (appeal) => {
        setAppeals(currentAppeals => {
            const updatedAppeals = [...currentAppeals, appeal];
            return updatedAppeals.sort((a, b) => new Date(b.deadlineDate) - new Date(a.deadlineDate));
        });
    };

    // In App.js
    const markAsDone = async (appealId) => {
        try {
            const response = await axios.post(`http://localhost:5244/api/appeals/markAsDone?appealId=${appealId}`);
            if (response.status === 200) {
                setAppeals(currentAppeals => {
                    const updatedAppeals = currentAppeals.filter(a => a.id !== appealId);
                    return updatedAppeals.sort((a, b) => new Date(b.deadlineDate) - new Date(a.deadlineDate));
                });
            }
        } catch (error) {
            console.error('Error marking appeal as done: ', error);
        }
    };


    return (
        <div className="App">
            <h1>Appeals Dashboard</h1>
            <AddAppeal addAppealToState={addAppealToState}/>
            <AppealsList appeals={appeals} markAsDone={markAsDone}/>
        </div>
    );
}

export default App;
