package com.example.ISU.Services;
import com.example.ISU.Models.Student;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

@Service
public class StudentService {
    final private List<Student> students = new ArrayList<>();

    public List<Student> getStudents(){
        return students;
    }

    public Optional<Student> getStudentById(int id){
        return students.stream().filter((student -> student.id == id)).findFirst();
    }

    public Student addStudent(Student newStudent){
        students.add(newStudent);
        return newStudent;
    }
}
