import React from 'react'

interface ButtonProps {}

// const StyledButton = styled.Button``

export function Button({ ...rest }: ButtonProps): JSX.Element {
  return <button {...rest} />
}
