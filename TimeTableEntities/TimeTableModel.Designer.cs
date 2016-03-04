﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

[assembly: EdmSchemaAttribute()]
#region EDM Relationship Metadata

[assembly: EdmRelationshipAttribute("TimetTableGenerator", "periodId_schedule_FK", "Period", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(TimeTableEntities.Period), "schedule", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(TimeTableEntities.Schedule), true)]

#endregion

namespace TimeTableEntities
{
    #region Contexts
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    public partial class TimeTableEntities : ObjectContext
    {
        #region Constructors
    
        /// <summary>
        /// Initializes a new TimeTableEntities object using the connection string found in the 'TimeTableEntities' section of the application configuration file.
        /// </summary>
        public TimeTableEntities() : base("name=TimeTableEntities", "TimeTableEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new TimeTableEntities object.
        /// </summary>
        public TimeTableEntities(string connectionString) : base(connectionString, "TimeTableEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        /// <summary>
        /// Initialize a new TimeTableEntities object.
        /// </summary>
        public TimeTableEntities(EntityConnection connection) : base(connection, "TimeTableEntities")
        {
            this.ContextOptions.LazyLoadingEnabled = true;
            OnContextCreated();
        }
    
        #endregion
    
        #region Partial Methods
    
        partial void OnContextCreated();
    
        #endregion
    
        #region ObjectSet Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Period> Periods
        {
            get
            {
                if ((_Periods == null))
                {
                    _Periods = base.CreateObjectSet<Period>("Periods");
                }
                return _Periods;
            }
        }
        private ObjectSet<Period> _Periods;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Schedule> Schedules
        {
            get
            {
                if ((_Schedules == null))
                {
                    _Schedules = base.CreateObjectSet<Schedule>("Schedules");
                }
                return _Schedules;
            }
        }
        private ObjectSet<Schedule> _Schedules;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Subject> Subjects
        {
            get
            {
                if ((_Subjects == null))
                {
                    _Subjects = base.CreateObjectSet<Subject>("Subjects");
                }
                return _Subjects;
            }
        }
        private ObjectSet<Subject> _Subjects;
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        public ObjectSet<Elective> Electives
        {
            get
            {
                if ((_Electives == null))
                {
                    _Electives = base.CreateObjectSet<Elective>("Electives");
                }
                return _Electives;
            }
        }
        private ObjectSet<Elective> _Electives;

        #endregion

        #region AddTo Methods
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Periods EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToPeriods(Period period)
        {
            base.AddObject("Periods", period);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Schedules EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToSchedules(Schedule schedule)
        {
            base.AddObject("Schedules", schedule);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Subjects EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToSubjects(Subject subject)
        {
            base.AddObject("Subjects", subject);
        }
    
        /// <summary>
        /// Deprecated Method for adding a new object to the Electives EntitySet. Consider using the .Add method of the associated ObjectSet&lt;T&gt; property instead.
        /// </summary>
        public void AddToElectives(Elective elective)
        {
            base.AddObject("Electives", elective);
        }

        #endregion

    }

    #endregion

    #region Entities
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="TimetTableGenerator", Name="Elective")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Elective : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Elective object.
        /// </summary>
        /// <param name="id">Initial value of the Id property.</param>
        public static Elective CreateElective(global::System.Int32 id)
        {
            Elective elective = new Elective();
            elective.Id = id;
            return elective;
        }

        #endregion

        #region Simple Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value, "Id");
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Int32 _Id;
        partial void OnIdChanging(global::System.Int32 value);
        partial void OnIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Code
        {
            get
            {
                return _Code;
            }
            set
            {
                OnCodeChanging(value);
                ReportPropertyChanging("Code");
                _Code = StructuralObject.SetValidValue(value, true, "Code");
                ReportPropertyChanged("Code");
                OnCodeChanged();
            }
        }
        private global::System.String _Code;
        partial void OnCodeChanging(global::System.String value);
        partial void OnCodeChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Subject
        {
            get
            {
                return _Subject;
            }
            set
            {
                OnSubjectChanging(value);
                ReportPropertyChanging("Subject");
                _Subject = StructuralObject.SetValidValue(value, true, "Subject");
                ReportPropertyChanged("Subject");
                OnSubjectChanged();
            }
        }
        private global::System.String _Subject;
        partial void OnSubjectChanging(global::System.String value);
        partial void OnSubjectChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Teacher
        {
            get
            {
                return _Teacher;
            }
            set
            {
                OnTeacherChanging(value);
                ReportPropertyChanging("Teacher");
                _Teacher = StructuralObject.SetValidValue(value, true, "Teacher");
                ReportPropertyChanged("Teacher");
                OnTeacherChanged();
            }
        }
        private global::System.String _Teacher;
        partial void OnTeacherChanging(global::System.String value);
        partial void OnTeacherChanged();

        #endregion

    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="TimetTableGenerator", Name="Period")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Period : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Period object.
        /// </summary>
        /// <param name="periodId">Initial value of the PeriodId property.</param>
        public static Period CreatePeriod(global::System.Int32 periodId)
        {
            Period period = new Period();
            period.PeriodId = periodId;
            return period;
        }

        #endregion

        #region Simple Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 PeriodId
        {
            get
            {
                return _PeriodId;
            }
            set
            {
                if (_PeriodId != value)
                {
                    OnPeriodIdChanging(value);
                    ReportPropertyChanging("PeriodId");
                    _PeriodId = StructuralObject.SetValidValue(value, "PeriodId");
                    ReportPropertyChanged("PeriodId");
                    OnPeriodIdChanged();
                }
            }
        }
        private global::System.Int32 _PeriodId;
        partial void OnPeriodIdChanging(global::System.Int32 value);
        partial void OnPeriodIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Start
        {
            get
            {
                return _Start;
            }
            set
            {
                OnStartChanging(value);
                ReportPropertyChanging("Start");
                _Start = StructuralObject.SetValidValue(value, true, "Start");
                ReportPropertyChanged("Start");
                OnStartChanged();
            }
        }
        private global::System.String _Start;
        partial void OnStartChanging(global::System.String value);
        partial void OnStartChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String End
        {
            get
            {
                return _End;
            }
            set
            {
                OnEndChanging(value);
                ReportPropertyChanging("End");
                _End = StructuralObject.SetValidValue(value, true, "End");
                ReportPropertyChanged("End");
                OnEndChanged();
            }
        }
        private global::System.String _End;
        partial void OnEndChanging(global::System.String value);
        partial void OnEndChanged();

        #endregion

        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("TimetTableGenerator", "periodId_schedule_FK", "schedule")]
        public EntityCollection<Schedule> Schedules
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Schedule>("TimetTableGenerator.periodId_schedule_FK", "schedule");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Schedule>("TimetTableGenerator.periodId_schedule_FK", "schedule", value);
                }
            }
        }

        #endregion

    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="TimetTableGenerator", Name="Schedule")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Schedule : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Schedule object.
        /// </summary>
        /// <param name="id">Initial value of the Id property.</param>
        public static Schedule CreateSchedule(global::System.Int32 id)
        {
            Schedule schedule = new Schedule();
            schedule.Id = id;
            return schedule;
        }

        #endregion

        #region Simple Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value, "Id");
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Int32 _Id;
        partial void OnIdChanging(global::System.Int32 value);
        partial void OnIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Class
        {
            get
            {
                return _Class;
            }
            set
            {
                OnClassChanging(value);
                ReportPropertyChanging("Class");
                _Class = StructuralObject.SetValidValue(value, true, "Class");
                ReportPropertyChanged("Class");
                OnClassChanged();
            }
        }
        private global::System.String _Class;
        partial void OnClassChanging(global::System.String value);
        partial void OnClassChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Teacher
        {
            get
            {
                return _Teacher;
            }
            set
            {
                OnTeacherChanging(value);
                ReportPropertyChanging("Teacher");
                _Teacher = StructuralObject.SetValidValue(value, true, "Teacher");
                ReportPropertyChanged("Teacher");
                OnTeacherChanged();
            }
        }
        private global::System.String _Teacher;
        partial void OnTeacherChanging(global::System.String value);
        partial void OnTeacherChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Subject
        {
            get
            {
                return _Subject;
            }
            set
            {
                OnSubjectChanging(value);
                ReportPropertyChanging("Subject");
                _Subject = StructuralObject.SetValidValue(value, true, "Subject");
                ReportPropertyChanged("Subject");
                OnSubjectChanged();
            }
        }
        private global::System.String _Subject;
        partial void OnSubjectChanging(global::System.String value);
        partial void OnSubjectChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> Period
        {
            get
            {
                return _Period;
            }
            set
            {
                OnPeriodChanging(value);
                ReportPropertyChanging("Period");
                _Period = StructuralObject.SetValidValue(value, "Period");
                ReportPropertyChanged("Period");
                OnPeriodChanged();
            }
        }
        private Nullable<global::System.Int32> _Period;
        partial void OnPeriodChanging(Nullable<global::System.Int32> value);
        partial void OnPeriodChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Day
        {
            get
            {
                return _Day;
            }
            set
            {
                OnDayChanging(value);
                ReportPropertyChanging("Day");
                _Day = StructuralObject.SetValidValue(value, true, "Day");
                ReportPropertyChanged("Day");
                OnDayChanged();
            }
        }
        private global::System.String _Day;
        partial void OnDayChanging(global::System.String value);
        partial void OnDayChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Elective
        {
            get
            {
                return _Elective;
            }
            set
            {
                OnElectiveChanging(value);
                ReportPropertyChanging("Elective");
                _Elective = StructuralObject.SetValidValue(value, true, "Elective");
                ReportPropertyChanged("Elective");
                OnElectiveChanged();
            }
        }
        private global::System.String _Elective;
        partial void OnElectiveChanging(global::System.String value);
        partial void OnElectiveChanged();

        #endregion

        #region Navigation Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("TimetTableGenerator", "periodId_schedule_FK", "Period")]
        public Period Periods
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Period>("TimetTableGenerator.periodId_schedule_FK", "Period").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Period>("TimetTableGenerator.periodId_schedule_FK", "Period").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<Period> PeriodsReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Period>("TimetTableGenerator.periodId_schedule_FK", "Period");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Period>("TimetTableGenerator.periodId_schedule_FK", "Period", value);
                }
            }
        }

        #endregion

    }
    
    /// <summary>
    /// No Metadata Documentation available.
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName="TimetTableGenerator", Name="Subject")]
    [Serializable()]
    [DataContractAttribute(IsReference=true)]
    public partial class Subject : EntityObject
    {
        #region Factory Method
    
        /// <summary>
        /// Create a new Subject object.
        /// </summary>
        /// <param name="id">Initial value of the Id property.</param>
        /// <param name="name">Initial value of the Name property.</param>
        public static Subject CreateSubject(global::System.Int32 id, global::System.String name)
        {
            Subject subject = new Subject();
            subject.Id = id;
            subject.Name = name;
            return subject;
        }

        #endregion

        #region Simple Properties
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=true, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.Int32 Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value, "Id");
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Int32 _Id;
        partial void OnIdChanging(global::System.Int32 value);
        partial void OnIdChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=false)]
        [DataMemberAttribute()]
        public global::System.String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                OnNameChanging(value);
                ReportPropertyChanging("Name");
                _Name = StructuralObject.SetValidValue(value, false, "Name");
                ReportPropertyChanged("Name");
                OnNameChanged();
            }
        }
        private global::System.String _Name;
        partial void OnNameChanging(global::System.String value);
        partial void OnNameChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Alias
        {
            get
            {
                return _Alias;
            }
            set
            {
                OnAliasChanging(value);
                ReportPropertyChanging("Alias");
                _Alias = StructuralObject.SetValidValue(value, true, "Alias");
                ReportPropertyChanged("Alias");
                OnAliasChanged();
            }
        }
        private global::System.String _Alias;
        partial void OnAliasChanging(global::System.String value);
        partial void OnAliasChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Int32> PeriodPerWeek
        {
            get
            {
                return _PeriodPerWeek;
            }
            set
            {
                OnPeriodPerWeekChanging(value);
                ReportPropertyChanging("PeriodPerWeek");
                _PeriodPerWeek = StructuralObject.SetValidValue(value, "PeriodPerWeek");
                ReportPropertyChanged("PeriodPerWeek");
                OnPeriodPerWeekChanged();
            }
        }
        private Nullable<global::System.Int32> _PeriodPerWeek;
        partial void OnPeriodPerWeekChanging(Nullable<global::System.Int32> value);
        partial void OnPeriodPerWeekChanged();
    
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty=false, IsNullable=true)]
        [DataMemberAttribute()]
        public global::System.String Elective
        {
            get
            {
                return _Elective;
            }
            set
            {
                OnElectiveChanging(value);
                ReportPropertyChanging("Elective");
                _Elective = StructuralObject.SetValidValue(value, true, "Elective");
                ReportPropertyChanged("Elective");
                OnElectiveChanged();
            }
        }
        private global::System.String _Elective;
        partial void OnElectiveChanging(global::System.String value);
        partial void OnElectiveChanged();

        #endregion

    }

    #endregion

}
