// src/App.js
import React from 'react';
import { View, StyleSheet, ScrollView } from 'react-native';
import AttendanceList from './components/AttendanceList';
import AttendanceForm from './components/AttendanceForm';

const App = () => {
    return (
        <ScrollView contentContainerStyle={styles.container}>
            <AttendanceForm />
            <AttendanceList teacherId={1} subjectId={1} groupId={1} date="2024-07-12T09:00:00" />
        </ScrollView>
    );
}

const styles = StyleSheet.create({
    container: {
        padding: 16,
        flexGrow: 1,
    },
});

export default App;
