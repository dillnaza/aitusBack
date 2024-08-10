import axios from 'axios';

const API_URL = 'https://localhost:5032/api';

export const login = async (email, password) => {
    return axios.post(`${API_URL}/Login`, { email, password });
};

export const getStudentSubjects = async (studentId) => {
    return axios.get(`${API_URL}/StudentMain/${studentId}`);
};

export const getStudentAttendance = async (studentId, subjectId) => {
    return axios.get(`${API_URL}/StudentMain/student/${studentId}/subject/${subjectId}`);
};

export const getTeacherSubjects = async (teacherId) => {
    return axios.get(`${API_URL}/TeacherMain/${teacherId}`);
};

export const getAttendanceDates = async (teacherId, subjectId, groupId) => {
    return axios.get(`${API_URL}/TeacherMain/${teacherId}/subject/${subjectId}/group/${groupId}/attendance-dates`);
};
