using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentService.DAL.DTO
{
    [Serializable]
    public record StudentWithGradesDTO
    {
        public int StudentId { get; init; }

        public string StudentName { get; init; }

        public string StudentSurname { get; init; }

        public int SubjectCount { get; init; }

        public double AverageGrade { get; init; }
    }
}
