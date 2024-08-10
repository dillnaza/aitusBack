import React, { useState, useEffect } from 'react';
import { View, Text, ScrollView } from 'react-native';
import { getAttendanceDates } from '../services/api';

const TeacherAttendanceScreen = ({ route }) => {
    const { teacherId, subjectId, groupId } = route.params;
    const [attendanceDates, setAttendanceDates] = useState([]);

    useEffect(() => {
        const fetchDates = async () => {
            const response = await getAttendanceDates(teacherId, subjectId, groupId);
            setAttendanceDates(response.data.AttendanceDates);
        };
        fetchDates();
    }, []);

    return (
        <ScrollView>
            {attendanceDates.map((date, index) => (
                <View key={index}>
                    <Text>{date}</Text>
                </View>
            ))}
        </ScrollView>
    );
};

export default TeacherAttendanceScreen;
