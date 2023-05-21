import { ReactNode } from 'react'
import { NavMenu } from './NavMenu'
import styled from 'styled-components'

const Container = styled.div``

interface LayoutProps {
  children: ReactNode
}

export function Layout({ children }: LayoutProps): JSX.Element {
  return (
    <div>
      <NavMenu />
      <Container role="main">{children}</Container>
    </div>
  )
}
