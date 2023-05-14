import { Calendar } from './Calendar'

export interface ApplicationUser {
  UserId: string
  Email: string
  FirstName: string
  LastName: string
  Password: string
  Role: string

  Calendars: Calendar[]
}

export interface UserLoginInput {
  Email: string
  Password: string
}

export interface UserRegisterInput {
  Email: string
  FirstName: string
  LastName: string
  Password: string
}
