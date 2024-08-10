import React, { useState, useEffect } from 'react';
import { View, Text, ScrollView } from 'react-native';
import { getStudentAttendance } from '../services/api';

const StudentAttendanceScreen = ({ route }) => {
    const { studentId, subjectId } = route.params;
    const [attendance, setAttendance] = useState([]);

    useEffect(() => {
        const fetchAttendance = async () => {
            const response = await getStudentAttendance(studentId, subjectId);
            setAttendance(response.data.Attendances);
        };
        fetchAttendance();
    }, []);

    return (
        <ScrollView>
            {attendance.map((record, index) => (
                <View key={index}>
                    <Text>{record.Date}</Text>
                    <Text>{record.Status}</Text>
                </View>
            ))}
        </ScrollView>
    );
};

export default StudentAttendanceScreen;
