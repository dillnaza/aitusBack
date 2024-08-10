import React, { useState, useEffect } from 'react';
import { View, Text, Button, ScrollView } from 'react-native';
import { getStudentSubjects } from '../services/api';

const StudentSubjectsScreen = ({ route, navigation }) => {
    const { studentId } = route.params;
    const [subjects, setSubjects] = useState([]);

    useEffect(() => {
        const fetchSubjects = async () => {
            const response = await getStudentSubjects(studentId);
            setSubjects(response.data.SubjectTeacher);
        };
        fetchSubjects();
    }, []);

    return (
        <ScrollView>
            {subjects.map((subject, index) => (
                <View key={index}>
                    <Text>{subject.SubjectName}</Text>
                    <Text>{subject.TeacherName}</Text>
                    <Button
                        title="View Attendance"
                        onPress={() => navigation.navigate('StudentAttendance', {
                            studentId,
                            subjectId: subject.SubjectId
                        })}
                    />
                </View>
            ))}
        </ScrollView>
    );
};

export default StudentSubjectsScreen;
