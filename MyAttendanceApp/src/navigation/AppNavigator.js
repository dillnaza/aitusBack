import React from 'react';
import { createStackNavigator } from '@react-navigation/stack';
import { NavigationContainer } from '@react-navigation/native';
import LoginScreen from '../components/LoginScreen';
import StudentSubjectsScreen from '../components/StudentSubjectsScreen';
import StudentAttendanceScreen from '../components/StudentAttendanceScreen';
import TeacherSubjectsScreen from '../components/TeacherSubjectsScreen';
import TeacherAttendanceScreen from '../components/TeacherAttendanceScreen';

const Stack = createStackNavigator();

const AppNavigator = () => (
    <NavigationContainer>
        <Stack.Navigator initialRouteName="Login">
            <Stack.Screen name="Login" component={LoginScreen} />
            <Stack.Screen name="StudentSubjects" component={StudentSubjectsScreen} />
            <Stack.Screen name="StudentAttendance" component={StudentAttendanceScreen} />
            <Stack.Screen name="TeacherSubjects" component={TeacherSubjectsScreen} />
            <Stack.Screen name="TeacherAttendance" component={TeacherAttendanceScreen} />
        </Stack.Navigator>
    </NavigationContainer>
);

export default AppNavigator;
