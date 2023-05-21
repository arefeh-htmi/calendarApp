interface ButtonProps {
  children: JSX.Element
}

// const StyledButton = styled.Button``

export function Button({ ...rest }: ButtonProps): JSX.Element {
  return <button {...rest} />
}
