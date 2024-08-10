import React, { useState, useEffect } from 'react';
import { View, Text, Button, ScrollView } from 'react-native';
import { getTeacherSubjects } from '../services/api';

const TeacherSubjectsScreen = ({ route, navigation }) => {
    const { teacherId } = route.params;
    const [subjects, setSubjects] = useState([]);

    useEffect(() => {
        const fetchSubjects = async () => {
            const response = await getTeacherSubjects(teacherId);
            setSubjects(response.data.SubjectGroup);
        };
        fetchSubjects();
    }, []);

    return (
        <ScrollView>
            {subjects.map((subject, index) => (
                <View key={index}>
                    <Text>{subject.SubjectName}</Text>
                    <Text>{subject.GroupName}</Text>
                    <Button
                        title="View Dates"
                        onPress={() => navigation.navigate('TeacherAttendance', {
                            teacherId,
                            subjectId: subject.SubjectId,
                            groupId: subject.GroupId
                        })}
                    />
                </View>
            ))}
        </ScrollView>
    );
};

export default TeacherSubjectsScreen;
