import { Fragment } from 'react'

interface Props {
  children: JSX.Element
}

export function HomePageLayout({ children }: Props): JSX.Element {
  return (
    <Fragment>
      <Header />
      <Container tag="main">{children}</Container>
    </Fragment>
  )
}
