import { ApplicationUser } from './ApplicationUser'

export interface Calendar {
  CalendarId: string
  Title?: string
  SubTitle?: string
  Owner: ApplicationUser
  Events: Event[]
  Users: ApplicationUser[]
}
