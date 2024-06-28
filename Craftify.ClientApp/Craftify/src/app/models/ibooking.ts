import { IUser } from "./iuser"
import { IWorker } from "./iworker"

export interface IBooking {
    id?: string
    workerId?: string
    worker?: IWorker
    customerId?: string
    customer?: IUser
    workDuration?: number
    startAt?: string
    endAt?: string
    status?: number
}
