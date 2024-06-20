import React, { useState } from 'react';
import Header from './components/Header';
import Main from './components/Main';
import 'bootstrap/dist/css/bootstrap.min.css';

const App = () => {
    const [useXml, setUseXml] = useState(false);
    const [tasks, setTasks] = useState([]);
    const [categories] = useState([
        { id: 1, name: 'Робота' },
        { id: 2, name: 'Навчання' },
        { id: 3, name: 'Особистий розвиток' },
        { id: 4, name: 'Здоров\'я і фітнес' },
        { id: 5, name: 'Сім\'я та відпочинок' },
    ]);

    const addTask = (task) => {
        setTasks([...tasks, task]);
    };

    const updateTaskStatus = (taskId, isCompleted) => {
        setTasks(tasks.map(task => task.id === taskId ? { ...task, isCompleted } : task));
    };

    const deleteTask = (taskId) => {
        setTasks(tasks.filter(task => task.id !== taskId));
    };

    return (
        <div className="container">
            <Header useXml={useXml} setUseXml={setUseXml} />
            <Main
                categories={categories}
                tasks={tasks}
                useXml={useXml}
                addTask={addTask}
                updateTaskStatus={updateTaskStatus}
                deleteTask={deleteTask}
            />
        </div>
    );
};

export default App;
