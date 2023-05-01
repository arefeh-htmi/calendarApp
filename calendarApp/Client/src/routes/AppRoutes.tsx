import ApiAuthorzationRoutes from './ApiAuthorizationRoutes'
import { Counter } from '../../Client/src/components/Counter'
import { FetchData } from '../components/FetchData'
import { Home } from '../components/Home'
import { Fragment } from 'react'

interface ApplicationRoute {
  index?: boolean
  element: JSX.Element
  path: string
  requireAuth?: boolean
}

export const AppRoutes: ApplicationRoute[] = [
  {
    path: '/',
    index: true,
    element: <Fragment />,
  },
  {
    path: '/calendar',
    requireAuth: true,
    element: <Fragment />,
  },
]
