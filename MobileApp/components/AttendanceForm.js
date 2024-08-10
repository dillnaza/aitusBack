// src/components/AttendanceForm.js
import React, { useState } from 'react';
import { View, TextInput, Button, StyleSheet, Text } from 'react-native';
import api from '../services/api';

const AttendanceForm = () => {
    const [teacherId, setTeacherId] = useState('');
    const [subjectId, setSubjectId] = useState('');
    const [groupId, setGroupId] = useState('');
    const [date, setDate] = useState('');
    const [message, setMessage] = useState('');

    const addAttendance = async () => {
        try {
            await api.post(`/TeacherMainController/${teacherId}/subject/${subjectId}/group/${groupId}/attendance-dates`, { date });
            setMessage('Attendance date added successfully');
        } catch (err) {
            setMessage(`Error: ${err.message}`);
        }
    };

    const deleteAttendance = async () => {
        try {
            await api.delete(`/TeacherMainController/${teacherId}/subject/${subjectId}/group/${groupId}/attendance-dates/${date}`);
            setMessage('Attendance date deleted successfully');
        } catch (err) {
            setMessage(`Error: ${err.message}`);
        }
    };

    return (
        <View style={styles.container}>
            <TextInput
                style={styles.input}
                placeholder="Teacher ID"
                value={teacherId}
                onChangeText={setTeacherId}
            />
            <TextInput
                style={styles.input}
                placeholder="Subject ID"
                value={subjectId}
                onChangeText={setSubjectId}
            />
            <TextInput
                style={styles.input}
                placeholder="Group ID"
                value={groupId}
                onChangeText={setGroupId}
            />
            <TextInput
                style={styles.input}
                placeholder="Date (YYYY-MM-DDTHH:MM:SS)"
                value={date}
                onChangeText={setDate}
            />
            <Button title="Add Attendance" onPress={addAttendance} />
            <Button title="Delete Attendance" onPress={deleteAttendance} />
            {message && <Text style={styles.message}>{message}</Text>}
        </View>
    );
};

const styles = StyleSheet.create({
    container: {
        padding: 16,
    },
    input: {
        height: 40,
        borderColor: 'gray',
        borderWidth: 1,
        marginBottom: 10,
        paddingHorizontal: 8,
    },
    message: {
        marginTop: 10,
        color: 'red',
    },
});

export default AttendanceForm;
