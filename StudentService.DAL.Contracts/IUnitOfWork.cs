﻿using StudentService.DAL.Contracts.Repositories;
using StudentService.DAL.Model;

namespace StudentService.DAL.Contracts
{
    public interface IUnitOfWork
    {
        IStudentRepository StudentRepository { get; }

        IGenericRepository<Subject> SubjectRepository { get; }

        IGenericRepository<Grade> GradeRepository { get; }  

        ITestRepository TestFillDbRepository { get; }

    }
}