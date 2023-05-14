import { InputHTMLAttributes } from 'react'
import styled from 'styled-components'

type InputProps = InputHTMLAttributes<HTMLInputElement>

const StyledInput = styled.input``

export function Input({ type, ...rest }: InputProps): JSX.Element {
  return <StyledInput type={type} {...rest} />
}
