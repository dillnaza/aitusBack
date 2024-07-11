// src/components/AttendanceList.js
import React, { useState, useEffect } from 'react';
import { View, Text, FlatList, StyleSheet } from 'react-native';
import api from '../services/api';

const AttendanceList = ({ teacherId, subjectId, groupId, date }) => {
    const [attendances, setAttendances] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchAttendances = async () => {
            try {
                const response = await api.get(`/TeacherMainController/${teacherId}/subject/${subjectId}/group/${groupId}/attendances/${date}`);
                setAttendances(response.data.studentAttendances);
            } catch (err) {
                setError(err.message);
            } finally {
                setLoading(false);
            }
        };

        fetchAttendances();
    }, [teacherId, subjectId, groupId, date]);

    if (loading) {
        return <Text>Loading...</Text>;
    }

    if (error) {
        return <Text>Error: {error}</Text>;
    }

    return (
        <View style={styles.container}>
            <FlatList
                data={attendances}
                keyExtractor={(item) => item.studentName}
                renderItem={({ item }) => (
                    <View style={styles.item}>
                        <Text>{item.studentName}</Text>
                        <Text>{item.status}</Text>
                    </View>
                )}
            />
        </View>
    );
};

const styles = StyleSheet.create({
    container: {
        flex: 1,
        padding: 16,
    },
    item: {
        padding: 16,
        borderBottomWidth: 1,
        borderBottomColor: '#ccc',
    },
});

export default AttendanceList;
