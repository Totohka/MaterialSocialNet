import { HTMLInputTypeAttribute } from 'react';
import { UseFormRegisterReturn } from 'react-hook-form';
import styles from './Input.module.scss';
import cx from 'classnames';
import { PropsWithClassName } from '../../types';

export type InputProps = {
  register: UseFormRegisterReturn<string>;
  placeholder: string;
  type?: HTMLInputTypeAttribute;
} & PropsWithClassName;

export const Input = ({
  register,
  placeholder,
  type = 'text',
  className,
}: InputProps) => {
  return (
    <div className={cx(styles.wrapper, className)}>
      <input
        {...register}
        className={styles.input}
        type={type}
        placeholder={placeholder}
      />
    </div>
  );
};
