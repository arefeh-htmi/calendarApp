import { Fragment, useEffect, useState } from 'react'
import {
  Collapse,
  Navbar,
  NavbarBrand,
  NavbarToggler,
  NavItem,
  NavLink,
} from 'reactstrap'
import { Link } from 'react-router-dom'
import { LoginMenu } from './api-authorization/LoginMenu'
import { ApplicationPaths } from '../../routes/ApiAuthorizationRoutes'

export function Header() {
  const [collapsed, setCollapsed] = useState(true)
  const [userName, setUserName] = useState(null)
  const [isAuthenticated, setIsAuthenticated] = useState(false)

  function toggleNavbar() {
    setCollapsed(!collapsed)
  }
  useEffect(() => {
    return () => {}
  }, [])

  return (
    <header>
      <Navbar
        className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3"
        container
        light
      >
        <NavbarBrand tag={Link} to="/">
          CalendarApp
        </NavbarBrand>
        <NavbarToggler onClick={toggleNavbar} className="mr-2" />
        <Collapse
          className="d-sm-inline-flex flex-sm-row-reverse"
          isOpen={!collapsed}
          navbar
        >
          <ul className="navbar-nav flex-grow">
            <NavItem>
              <NavLink tag={Link} className="text-dark" to="/">
                Home
              </NavLink>
            </NavItem>
            {isAuthenticated ? (
              <AuthenticatedUser userName={userName} />
            ) : (
              <NonAuthenticatedUser />
            )}
          </ul>
        </Collapse>
      </Navbar>
    </header>
  )
}

interface AuthenticatedUserProps {
  userName: string
  logoutPath: string
  logoutState: any
}

function AuthenticatedUser({
  userName,
  logoutPath,
  logoutState,
}: AuthenticatedUserProps): JSX.Element {
  return (
    <Fragment>
      <NavItem>
        <NavLink tag={Link} className="text-dark" to={ApplicationPaths.Profile}>
          Hello {userName}
        </NavLink>
      </NavItem>
      <NavItem>
        <NavLink
          replace
          tag={Link}
          className="text-dark"
          to={logoutPath}
          state={logoutState}
        >
          Logout
        </NavLink>
      </NavItem>
    </Fragment>
  )
}

interface NonAuthenticatedUser {
  registerPath: string
  loginPath: string
}

function NonAuthenticatedUser({
  registerPath,
  loginPath,
}: NonAuthenticatedUser): JSX.Element {
  return (
    <Fragment>
      <NavItem>
        <NavLink tag={Link} className="text-dark" to={registerPath}>
          Register
        </NavLink>
      </NavItem>
      <NavItem>
        <NavLink tag={Link} className="text-dark" to={loginPath}>
          Login
        </NavLink>
      </NavItem>
    </Fragment>
  )
}
