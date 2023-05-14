import { ApplicationUser } from "./ApplicationUser"
import { Calendar } from "./Calendar"
import { Location } from "./Location"

export interface Event {
  Id: string
  Name?: string
  Description?: string
  StartTime: string //date
  EndTime: string //Date

  Location: Location //Location
  CreatorId: string // Creator applicationUser
  Calendar: Calendar
  Users: ApplicationUser[]
}
