package com.example.ISU.Controllers;

import com.example.ISU.Models.Student;
import com.example.ISU.Services.StudentService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;
import java.util.List;

@RestController
public class ISUController {

    @Autowired
    private StudentService studentService;

    @GetMapping("/students")
    public List<Student> getStudents() {
        return studentService.getStudents();
    }

    @GetMapping("/students/{id}")
    public Student getStudentById(@PathVariable int id){
        var result = studentService.getStudentById(id);
        return result.orElse(null);
    }

    @PostMapping("/createStudent")
    public Student createNewStudent(@RequestParam String name, @RequestParam int id){
        var student = new Student();
        student.id = id;
        student.name = name;
        return studentService.addStudent(student);
    }

    @PostMapping("/addStudent")
    public Student addNewStudent(@RequestBody Student student){
        return studentService.addStudent(student);
    }

}