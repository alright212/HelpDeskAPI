import React, {useState} from 'react';
import axios from 'axios';

function AddAppeal({addAppealToState}) {
    const [description, setDescription] = useState('');
    const [deadlineDate, setDeadlineDate] = useState('');

    const handleSubmit = async (event) => {
        event.preventDefault();
        try {
            const response = await axios.post('http://localhost:5244/api/appeals/add', {
                description,
                deadlineDate
            });
            addAppealToState(response.data);
            setDescription('');
            setDeadlineDate('');
        } catch (error) {
            console.error('Error adding appeal: ', error);
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <input
                type="text"
                value={description}
                onChange={e => setDescription(e.target.value)}
                placeholder="Description"
                required
            />
            <input
                type="date"
                value={deadlineDate}
                onChange={e => setDeadlineDate(e.target.value)}
                required
            />
            <button type="submit">Add Appeal</button>
        </form>
    );
}

export default AddAppeal;
