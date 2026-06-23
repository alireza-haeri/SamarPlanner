# Samar Planner Web Application

## 1\. Program Description

this application help user to manage goals in the life.  
user define the goals and the own tasks.

## 2\. Entities

### User

Represent a person who use the application

#### Attributes

```
- UserId
- PhoneNumber
- HashPassword
```

* * *

### Goal

Represents goals of users who like a tree

#### Attributes

```
- Title
- Description
- Priority
- Type
- ParentGoalId
```

#### Notes

```
- Priority can be:
  - High
  - Medium
  - Low
  - None
- Type can be:
  - Long
  - Medium
  - Short
```

* * *

### Task

Represent tasks of user who created by a user

#### Attributes

```
- Title
- Description
- RepeatPattern
- DefaultTime
- Priority
- Type
- ParentGoalId
- SoftDeleted
```

#### Notes

```
- RepeatPattern has:
  - Pattern can be:
    - Daily
    - WeeklyOnDays
    - MonthlyOnDays
  - Interval
  - WeekDays
  - MonthDays
- Priority can be:
  - High
  - Medium
  - Low
  - None
- Type can be:
  - Task
  - Event
```

- `DefaultTime` is the default time applied to all occurrences of this task. An occurrence can override this with its own specific time.

* * *

### TaskOccurrence

Represents a specific instance of a task on a particular date.

#### Attributes
- TaskId
- Date
- Time
- Status
- Score
- IsSkipped
- SoftDeleted

#### Notes
- Status can be:
  - Pending
  - Done
  - NotDone
  - AlmostDone
- Time is optional. If not provided, the occurrence uses the DefaultTime from its parent Task.
- Score is optional and independent of the Status. It is a user-defined arbitrary value.
---

### Report

Represent a note who user write about a range of own time

#### Attributes

```
- Title
- Note
- Score
- PeriodStart
- PeriodEnd
- Type
```

#### Notes

```
- Type can be:
  - Yearly
  - Monthly
  - Weekly
  - Daily
```

* * *

### Note

Represents a note who user write about something by simplest way for read some time

#### Attribute

```
- Title
- Note
- DateTime
```

* * *

## 3\. Behavior

### User Behaviors

- User can login or register in the same time
- User can view all own Goals
- User can create a Goal
- User can update a Goal
- User can delete a Goal
- User can view all own task
- User can filter own tasks by day
- User can filter own tasks by month
- User can create a task
- User can update a task
- User can soft delete a task
- User can view all soft deleted task
- User can delete soft deleted task
- User can restore a soft deleted task
- User can create a new occurrence for an existing task (even if the task has no repeat pattern)
- User can set status for an occurrence
- User can set an optional score for an occurrence (regardless of its status)
- User can update the time of an occurrence (if not set, it uses the task's default time)
- User can soft delete an occurrence
- User can restore a soft deleted occurrence
- User can create a report
- User can update a report
- User can delete a report
- User can view all own report
- User can filter report by date
- User can filter report by week
- User can filter report by month
- User can filter report by year
- User can view all reports score
- User can filter report score by day
- User can filter report score by week
- User can filter report score by month
- User can filter report score by year
- User can create a note
- User can update a note
- User can delete a note
- User can view a note detail

### Goal Behaviors

- Goal can be created
- Goal can be update
- Goal can be delete
- Goal can attach to tasks
- Goal can be child of a goal

### Task Behaviors

- Task can be created by a user
- Task can be update
- Task can be soft delete
- Task can restore from soft delete
- Task can be deleted
- Task can have multiple occurrences (even without a repeat pattern)
- Occurrence can be created for a task
- Occurrence can be soft deleted
- Occurrence can be restored from soft delete
- Occurrence can have an optional score set by the user

### Report Behaviors

- Report can create by a user for a day
- Report can create by a user for a week
- Report can create by a user for a month
- Report can create by a user for a year
- Report can update
- Report can delete

### Note Behaviors

- Note can create by a user
- Note can update
- Note can delete

## 4\. Basic Rules

- A task must always belong to a user
- A task must have a title
- A user can only manage own tasks
- A goal must always belong to a user
- A goal must have a title
- A user can only manage own goals
- A report must always belong to a user
- A report must have a note
- A user can only manage own reports
- A note must always belong to a user
- A note must have a note
- A user can only manage own notes
- A soft deleted task can not be update
- A task can have zero or more occurrences
- An occurrence must always belong to a task
- If an occurrence does not have a specific time set, it automatically uses the DefaultTime of its parent task
- An occurrence's score is completely optional and can be set or updated at any time by the user
- A soft deleted occurrence cannot be updated